using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Windows.Shapes;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using QuestPDF.Previewer;

namespace ResumeBuilderUI
{
    [Serializable]
    public class ResumeBuilder
    {
        const string textColorCVLight = "EFEFEF";
        const string textCVAccentColor = "002256";
        const string pathToResoursesFolder = "D:\\Programming\\VisualStudioProjects\\ResumeBuilderUI\\ResumeBuilderUI\\bin\\Debug\\net6.0-windows\\resources\\";

        public string Name { get; set; }
        public string Title { get; set; }
        public List<string> LanguagesList { get; private set; }
        public List<ProffessionalAffiliation> AffiliationsList { get; set; }
        public List<string> RelevantSkills { get; set; }
        public List<Employment> EmploymentsList { get; set; }
        public Dictionary<string, string> ContactsList { get; set; }

        public ResumeBuilder(string name,
            string title, List<string> languagesList,
            List<ProffessionalAffiliation> affiliationsList, List<string> relevantskills,
            List<Employment> employmentsList, Dictionary<string, string> contactsList)
        {
            Name = name;
            Title = title;
            LanguagesList = languagesList;
            AffiliationsList = affiliationsList;
            RelevantSkills = relevantskills;
            EmploymentsList = employmentsList;
            ContactsList = contactsList;
        }

        public ResumeBuilder(string pathToResumeResourceFile)
        {
            AffiliationsList = new List<ProffessionalAffiliation>();
            LanguagesList = new List<string>();
            EmploymentsList = new List<Employment>();
            try
            {
                StreamReader resourceReader = new StreamReader(pathToResumeResourceFile);
                string line = resourceReader.ReadLine();
                while (line != null)
                {
                    switch (line)
                    {
                        case "[Name]":
                            line = resourceReader.ReadLine();
                            Name = line ?? "Ivan Ivanov";
                            break;
                        case "[Title]":
                            line = resourceReader.ReadLine();
                            Title = line ?? "Engineer";
                            break;
                        case "[Professional Affiliations]":
                            line = resourceReader.ReadLine();
                            if ((line != null) && (!line.StartsWith('[')) && (!line.Equals("")))
                                do
                                {
                                    AffiliationsList.Add(ProffessionalAffiliation.Parse(line));
                                    line = resourceReader.ReadLine();
                                } while ((line != null) && (!line.StartsWith('[')) && (!line.Equals("")));
                            break;
                        case "[Languages]":
                            line = resourceReader.ReadLine();
                            if ((line != null) && (!line.StartsWith('[')) && (!line.Equals("")))
                                do
                                {
                                    LanguagesList.Add(line);
                                    line = resourceReader.ReadLine();
                                } while ((line != null) && (!line.StartsWith('[')) && (!line.Equals("")));
                            break;
                        case "[Experience]":
                            line = resourceReader.ReadLine();
                            if ((line != null) && (!line.StartsWith('[')) && (!line.Equals("")))
                                do
                                {
                                    EmploymentsList.Add(new Employment(line.Substring(1, (line.Length - 2))));
                                    line = resourceReader.ReadLine();
                                    while (line != null && line.StartsWith('-'))
                                    {
                                        EmploymentsList.Last().ExperiencesList.Add(new Experience(
                                            line.Substring(1), resourceReader.ReadLine() ?? " "));
                                        line = resourceReader.ReadLine();
                                    }
                                } while ((line != null) && (!line.StartsWith('[')) && (!line.Equals("")));
                            break;
                    }
                    line = resourceReader.ReadLine();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

        }

        static IContainer LeftColumnMain(IContainer container)
        {
            return container
                .Padding(5)
                .AlignLeft()
                .AlignTop();
        }

        static IContainer ToolsBody(IContainer container)
        {
            return container
                .Background("#0C2C5E")
                .AlignCenter()
                .AlignTop();
        }

        private List<string> GetEmployersList(List<Employment> employments)
        {
            employments = Employment.Sort(employments);
            List<string> employersList = new List<string>();
            foreach (Employment element in employments)
            {
                if (!employersList.Contains(element.Employer))
                {
                    employersList.Add(element.Employer);
                }
            }
            return employersList;
        }

        private DateTime GetEmployerStartDate(List<Employment> employments, string employer)
        {
            DateTime startDate = DateTime.MaxValue;
            foreach (Employment element in employments)
            {
                if (element.Employer == employer && element.StartDate < startDate)
                {
                    startDate = element.StartDate;
                }
            }
            return startDate;
        }

        private DateTime GetEmployerEndDate(List<Employment> employments, string employer)
        {
            DateTime endDate = DateTime.MinValue;
            foreach (Employment element in employments)
            {
                if (element.Employer == employer && element.EndDate > endDate)
                {
                    endDate = element.EndDate;
                }
            }
            return endDate;
        }

        //Adds to a RelevantExperience of each employment Experiences that are unique to theese employments
        private List<Experience> FindUniqueRelevantExperience(Employment employment)
        {
            List<Experience> uniqueRelevantExperience = new List<Experience>();
            //bool isUniqueExperience = false;
            //foreach (Employment employment in EmploymentsList)
            //{
            //    foreach (Experience experience in employment.ExperiencesList)
            //    {
            //        if (RelevantSkills.Contains(experience.Tag))
            //        {
            //            isUniqueExperience= true;
            //            foreach (Employment otherEmployment in EmploymentsList)
            //            {
            //                if (!employment.Equals(otherEmployment))
            //                {
            //                    foreach (Experience otherExperience in otherEmployment.ExperiencesList)
            //                    {
            //                        if (experience.Tag.Equals(otherExperience.Tag))
            //                        {
            //                            isUniqueExperience = false;
            //                        }
            //                    }
            //                }
            //            }
            //        }
            //        if (isUniqueExperience)
            //        {
            //            uniqueRelevantExperience.Add(experience.Description);
            //            isUniqueExperience= false;
            //        }
            //    }
            //    employment.RelevantExperience = new List<string>(uniqueRelevantExperience);
            //    uniqueRelevantExperience.Clear();
            //}
            return uniqueRelevantExperience;
        }

        public void BuildResume(string pathToOutputGeneratedResume)
        {
            QuestPDF.Settings.License = LicenseType.Community;
            CultureInfo.CurrentCulture = CultureInfo.GetCultureInfo("en-US");
            var resume = Document.Create(container =>
            {
                _ = container.Page(page =>
                {
                    //Page Settings
                    page.Size(PageSizes.A4);
                    page.Margin(0, Unit.Centimetre);
                    page.PageColor(Colors.White);
                    page.DefaultTextStyle(x => x.FontSize(20).FontFamily(Fonts.Calibri));

                    page.Content().Row(row =>
                    {
                        //Left Row
                        row.ConstantItem(250)
                        .Background("002256")
                        .ExtendVertical()
                        .Padding(10)
                        .ShowOnce()
                        .Column(column =>
                        {
                            //Avatar, Name and Title
                            column.Item().Table(table =>
                            {
                                table.ColumnsDefinition(columns =>
                                {
                                    columns.RelativeColumn();
                                    columns.RelativeColumn();
                                });
                                //Avatar
                                table.Cell().Row(1).Column(1).Element(LeftColumnMain).Image(pathToResoursesFolder + "Avatar Circle.png");
                                //Name and Title
                                table.Cell().Row(1).Column(2).Element(LeftColumnMain).Text(text =>
                                {
                                    text.DefaultTextStyle(x => x.FontSize(20).FontColor(textColorCVLight));
                                    text.Line(Name).ExtraBold();
                                    text.Line(Title).FontSize(18);
                                });
                            });
                            //Summary
                            column.Item().Table(table =>
                            {
                                table.ColumnsDefinition(columns =>
                                {
                                    columns.ConstantColumn(90);
                                    columns.RelativeColumn();
                                });
                                table.Cell().Row(1).Column(1).Element(LeftColumnMain).Text("Summary").FontColor(textColorCVLight).FontSize(16);
                                table.Cell().Row(1).Column(2).AlignMiddle().PaddingVertical(15).LineHorizontal(1).LineColor(textColorCVLight);
                            });
                            column.Item().Text("Experienced professional in mechanical design, robotics and mechatronics with 4+ years of industry experience in industrial automation and product development with an innovative, hands-on approach and a can-do attitude.")
                            .FontColor(textColorCVLight).FontSize(11).Light();
                            //Tools
                            column.Item().Table(table =>
                            {
                                table.ColumnsDefinition(columns =>
                                {
                                    columns.ConstantColumn(50);
                                    columns.RelativeColumn();
                                });
                                table.Cell().Row(1).Column(1).Element(LeftColumnMain).Text("Tools").FontColor(textColorCVLight).FontSize(16);
                                table.Cell().Row(1).Column(2).AlignMiddle().PaddingVertical(15).LineHorizontal(1).LineColor(textColorCVLight);
                            });
                            //Toolset 1
                            column.Item().Table(table =>
                            {
                                table.ColumnsDefinition(columns =>
                                {
                                    columns.RelativeColumn();
                                });
                                table.Cell().Row(1).Column(1).Element(ToolsBody).Text("CAD").FontSize(14).FontColor(textColorCVLight).ExtraBold();
                                table.Cell().Row(2).Column(1).Element(ToolsBody).Text("Solidworks, Compas-3D, Creo Parametric, AutoCAD, Siemens NX").FontSize(11).FontColor(textColorCVLight);
                            });
                            //Offset
                            column.Item().Text("").FontSize(5);
                            //Toolset 2
                            column.Item().Table(table =>
                            {
                                table.ColumnsDefinition(columns =>
                                {
                                    columns.RelativeColumn();
                                });
                                table.Cell().Row(1).Column(1).Element(ToolsBody).Text("Simulation").FontSize(14).FontColor(textColorCVLight).ExtraBold();
                                table.Cell().Row(2).Column(1).Element(ToolsBody).Text("Matlab, MathCAD, SolidWorks Simulation, Gazeebo, V-Rep, Ansys, RoboDK").FontSize(11).FontColor(textColorCVLight);
                            });
                            //Offset
                            column.Item().Text("").FontSize(5);
                            //Toolset 3
                            column.Item().Table(table =>
                            {
                                table.ColumnsDefinition(columns =>
                                {
                                    columns.RelativeColumn();
                                });
                                table.Cell().Row(1).Column(1).Element(ToolsBody).Text("CAM/CAE/PLM").FontSize(14).FontColor(textColorCVLight).ExtraBold();
                                table.Cell().Row(2).Column(1).Element(ToolsBody).Text("Scanform, Geomagic, Fusion 360, Soyuz-PLM, FreeMill").FontSize(11).FontColor(textColorCVLight);
                            });
                            //Offset
                            column.Item().Text("").FontSize(5);
                            //Toolset 4
                            column.Item().Table(table =>
                            {
                                table.ColumnsDefinition(columns =>
                                {
                                    columns.RelativeColumn();
                                });
                                table.Cell().Row(1).Column(1).Element(ToolsBody).Text("Programming").FontSize(14).FontColor(textColorCVLight).ExtraBold();
                                table.Cell().Row(2).Column(1).Element(ToolsBody).Text("Visual Studio (ASP.NET, C#), Android Studio (Java), Arduino IDE, PyCharm (Python), SQL").FontSize(11).FontColor(textColorCVLight);
                            });
                            //Languages
                            column.Item().Table(table =>
                            {
                                table.ColumnsDefinition(columns =>
                                {
                                    columns.ConstantColumn(100);
                                    columns.RelativeColumn();
                                });
                                table.Cell().Row(1).Column(1).Element(LeftColumnMain).Text("Languages").FontColor(textColorCVLight).FontSize(16);
                                table.Cell().Row(1).Column(2).AlignMiddle().PaddingVertical(15).LineHorizontal(1).LineColor(textColorCVLight);
                            });
                            foreach (string language in LanguagesList)
                            {
                                column.Item().Text(text =>
                                {
                                    text.DefaultTextStyle(x => x.FontColor(textColorCVLight).FontSize(11));
                                    text.Element().PaddingBottom(-6).Height(20).Image(pathToResoursesFolder + language.Substring(0, language.IndexOf(' ')) + ".png").FitHeight();
                                    text.Span("   " + language);
                                });
                                column.Item().Text("").FontSize(10);
                            }
                            //Contacts
                            column.Item().Table(table =>
                            {
                                table.ColumnsDefinition(columns =>
                                {
                                    columns.ConstantColumn(75);
                                    columns.RelativeColumn();
                                });
                                table.Cell().Row(1).Column(1).Element(LeftColumnMain).Text("Contacts").FontColor(textColorCVLight).FontSize(16);
                                table.Cell().Row(1).Column(2).AlignMiddle().PaddingVertical(15).LineHorizontal(1).LineColor(textColorCVLight);
                            });
                            foreach (string contactOption in ContactsList.Keys)
                            {
                                column.Item().Text(text =>
                                {
                                    text.DefaultTextStyle(x => x.FontColor(textColorCVLight).FontSize(11));
                                    try
                                    {
                                        text.Element().PaddingBottom(-6).Height(20).Image(pathToResoursesFolder + contactOption + ".png").FitHeight();
                                    }
                                    catch (Exception ex)
                                    {
                                        text.Span(contactOption + " ");
                                    }
                                    text.Span("   " + ContactsList.GetValueOrDefault(contactOption));
                                });
                                column.Item().Text("").FontSize(5);
                            }
                        });
                        //Right Row
                        row.RelativeItem()
                        .Padding(0)
                        //Work Experience
                        .Column(column =>
                        {
                            column.Item().Table(table =>
                            {
                                table.ColumnsDefinition(columns =>
                                {
                                    columns.ConstantColumn(135);
                                    columns.RelativeColumn();
                                });
                                table.Cell().Row(1).Column(1).Element(LeftColumnMain).Text("Work Experience").FontColor(textCVAccentColor).FontSize(16);
                                table.Cell().Row(1).Column(2).AlignMiddle().PaddingVertical(20).LineHorizontal(1).LineColor(textCVAccentColor);
                            });
                            //Experiences
                            foreach (string employer in GetEmployersList(EmploymentsList))
                            {
                                column.Item().AlignRight().PaddingHorizontal(10).Text(employer).FontColor(textCVAccentColor).FontSize(13);
                                column.Item().AlignRight().PaddingHorizontal(10)
                                .Text(GetEmployerStartDate(EmploymentsList, employer).ToString("Y") + " - " + GetEmployerEndDate(EmploymentsList, employer).ToString("Y"))
                                .FontColor("282828").FontSize(9);
                                foreach (Employment position in EmploymentsList)
                                {
                                    if (position.Employer == employer)
                                    {
                                        column.Item().AlignLeft().PaddingHorizontal(5).Text(position.Title).FontColor(textCVAccentColor).FontSize(12);
                                        column.Item().AlignLeft().PaddingHorizontal(5)
                                        .Text(position.StartDate.ToString("Y") + " - " + position.EndDate.ToString("Y"))
                                        .FontColor("282828").FontSize(9);
                                        foreach (Experience experience in FindUniqueRelevantExperience(position))
                                        {
                                            column.Item().AlignLeft().PaddingHorizontal(15).Text("• " + experience.Description).FontSize(10).FontColor("000000");
                                        }
                                    }
                                }
                            }

                            //Education
                            column.Item().Table(table =>
                            {
                                table.ColumnsDefinition(columns =>
                                {
                                    columns.ConstantColumn(90);
                                    columns.RelativeColumn();
                                });
                                table.Cell().Row(1).Column(1).Element(LeftColumnMain).Text("Education").FontColor(textCVAccentColor).FontSize(16);
                                table.Cell().Row(1).Column(2).AlignMiddle().PaddingVertical(20).LineHorizontal(1).LineColor(textCVAccentColor);
                            });
                            //University
                            column.Item().AlignRight().PaddingHorizontal(10).Text("ITMO University").FontColor(textCVAccentColor).FontSize(13);
                            column.Item().AlignRight().PaddingHorizontal(10).Text("September 2014 - August 2020").FontColor("282828").FontSize(9);
                            //First Degree
                            column.Item().AlignLeft().PaddingHorizontal(5).Text(text =>
                            {
                                text.DefaultTextStyle(x => x.FontSize(12).FontColor(textCVAccentColor));
                                text.Span("Master of Science").ExtraBold();
                                text.Span(" - Intellectual Technologies In Robotics");
                            });
                            column.Item().AlignLeft().PaddingHorizontal(5).Text(text =>
                            {
                                text.DefaultTextStyle(x => x.FontSize(9).FontColor("282828"));
                                text.Span("September 2018 - August 2020 - ");
                                text.Span("Diploma with Honours").FontColor("FF0000");
                            });
                            column.Item().AlignLeft().PaddingHorizontal(5).Text("Thesis Title: Development of wearable sign language Interpreter").FontColor("000000").FontSize(11);
                            column.Item().Text("").FontSize(10);
                            //Second Degree
                            column.Item().AlignLeft().PaddingHorizontal(5).Text(text =>
                            {
                                text.DefaultTextStyle(x => x.FontSize(12).FontColor(textCVAccentColor));
                                text.Span("Bachelor of Science").ExtraBold();
                                text.Span(" - Mechatronics and Robotics");
                            });
                            column.Item().AlignLeft().PaddingHorizontal(5).Text("September 2014 - August 2018 - ").FontSize(9).FontColor("282828");
                            column.Item().AlignLeft().PaddingHorizontal(5).Text("Thesis Title: Development of a motion restriction system for interaction with objects of virtual and augmented reality").FontColor("000000").FontSize(11);
                            // Professional Affiliations
                            column.Item().Table(table =>
                            {
                                table.ColumnsDefinition(columns =>
                                {
                                    columns.ConstantColumn(190);
                                    columns.RelativeColumn();
                                });
                                table.Cell().Row(1).Column(1).Element(LeftColumnMain).Text("Professional Affiliations").FontColor(textCVAccentColor).FontSize(16);
                                table.Cell().Row(1).Column(2).AlignMiddle().PaddingVertical(20).LineHorizontal(1).LineColor(textCVAccentColor);
                            });
                            foreach (ProffessionalAffiliation affiliation in AffiliationsList)
                            {
                                column.Item().PaddingHorizontal(5).Text(affiliation.ToString()).FontSize(10).FontColor("000000");
                            }
                        });
                    });
                });
            });
            resume.GeneratePdf(pathToOutputGeneratedResume);
            //resume.ShowInPreviewer();
        }
    }
}
