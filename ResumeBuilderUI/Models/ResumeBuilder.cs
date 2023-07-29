﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace ResumeBuilderUI.Models
{
    [Serializable]
    public class ResumeBuilder
    {
        const string textColorCVLight = "EFEFEF";
        const string textCVAccentColor = "002256";
        const string pathToResoursesFolder = "D:\\Programming\\VisualStudioProjects\\ResumeBuilderUI\\ResumeBuilderUI\\bin\\Debug\\net6.0-windows\\resources\\";

        public string Name { get; set; }
        public string Title { get; set; }
        public string Summary { get; set; }
        public List<Skillset> RelevantSkillsets { get; set; }
        public List<string> RelevantSkills { get; set; }
        public Dictionary<string, string> LanguagesList { get; private set; }
        public List<Employment> EmploymentsList { get; set; }
        public List<Education> EducationsList { get; set; }
        public Dictionary<string, string> ContactsList { get; set; }
        public List<ProffessionalAffiliation> AffiliationsList { get; set; }

        public ResumeBuilder(string name,
            string title, string summary, Dictionary<string, string> languagesList,
            List<ProffessionalAffiliation> affiliationsList, List<string> relevantskills,
            List<Skillset> relevantSkillsets, List<Employment> employmentsList, Dictionary<string, 
                string> contactsList)
        {
            Name = name;
            Title = title;
            Summary = summary;
            LanguagesList = languagesList;
            AffiliationsList = affiliationsList;
            RelevantSkills = relevantskills;
            RelevantSkillsets = relevantSkillsets;
            EmploymentsList = employmentsList;
            ContactsList = contactsList;
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
            foreach(Experience experience in employment.ExperiencesList)
            {
                if (RelevantSkills.Contains(experience.Tag))
                {
                    uniqueRelevantExperience.Add(experience);
                }
            }
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
                            column.Item().Text(Summary)
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
                            foreach (Skillset toolsCategoty in RelevantSkillsets)
                            {
                                column.Item().Table(table =>
                                {
                                    table.ColumnsDefinition(columns =>
                                    {
                                        columns.RelativeColumn();
                                    });
                                    table.Cell().Row(1).Column(1).Element(ToolsBody).AlignCenter().Text(toolsCategoty.MainSkill).FontSize(14).FontColor(textColorCVLight).ExtraBold();
                                    table.Cell().Row(2).Column(1).Element(ToolsBody).AlignLeft().Text(String.Join(',',toolsCategoty.SkillsList)).FontSize(11).FontColor(textColorCVLight);
                                });
                                column.Item().Text("").FontSize(5);
                            }
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
                            foreach (string language in LanguagesList.Keys)
                            {
                                column.Item().Text(text =>
                                {
                                    text.DefaultTextStyle(x => x.FontColor(textColorCVLight).FontSize(11));
                                    text.Element().PaddingBottom(-6).Height(20).Image(pathToResoursesFolder + language.Substring(0, language.IndexOf(' ')) + ".png").FitHeight();
                                    text.Span("   " + language +" - "+ LanguagesList.GetValueOrDefault(language));
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
            new Process { StartInfo = new ProcessStartInfo(pathToOutputGeneratedResume) { UseShellExecute = true } }.Start();
            //resume.ShowInPreviewer();
        }
    }
}
