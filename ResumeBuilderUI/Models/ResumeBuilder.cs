using System;
using System.Resources;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using MahApps.Metro.IconPacks;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using System.Linq;
using QuestPDF.Drawing.Exceptions;
using System.Net.Http.Headers;
using System.Collections.ObjectModel;
using static System.Net.Mime.MediaTypeNames;
using SkiaSharp;
using System.Windows.Controls;

namespace ResumeBuilderUI.Models
{
    /// <summary>
    /// Builder class that gathers relevent information from ApplicantProfile and creates CV based on it. Supports PDF output.
    /// </summary>
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
        public List<Skill> RelevantSkills { get; set; }
        public List<Language> LanguagesList { get; private set; }
        public List<Employment> EmploymentsList { get; set; }
        public List<Experience> ExperiencesList { get; set; }
        public List<Education> EducationsList { get; set; }
        public List<Contact> ContactsList { get; set; }
        public List<ProffessionalAffiliation> AffiliationsList { get; set; }

        public ResumeBuilder(string name,
            string title, string summary, List<Language> languagesList,
            List<ProffessionalAffiliation> affiliationsList, List<Skill> relevantskills,
            List<Skillset> relevantSkillsets, List<Employment> employmentsList, List<Education> educationList, List<Contact> contactsList)
        {
            Name = name;
            Title = title;
            Summary = summary;
            LanguagesList = languagesList;
            AffiliationsList = affiliationsList;
            RelevantSkills = relevantskills;
            RelevantSkillsets = relevantSkillsets;
            EmploymentsList = employmentsList;
            EducationsList = educationList;
            ContactsList = contactsList;
            ExperiencesList = FormSortedListOfRelevantExperiences();
        }
        static IContainer LeftColumnMain(IContainer container)
        {
            return container
                .Padding(5)
                .AlignLeft();
        }

        private List<Education> GetEducationsList(List<Education> educations)
        {
            educations = Education.Sort(educations);
            return educations;
        }

        private List<Skillset> GetToolsList(List<Skillset> tools)
        {
            Dictionary<Skillset, int> toSortDictionary = new Dictionary<Skillset, int>();
            List<Skillset> toShowSkillsets = new List<Skillset>();
            int counter;
            foreach (Skillset skillset in tools)
            {
                counter = 0;
                foreach(Skill skill in skillset.SkillsList)
                {
                    if(skill.IsSelected)
                    {
                        counter++;
                    }
                }
                toSortDictionary.Add(skillset, counter);
            }
            var sortedDictionary = toSortDictionary.OrderByDescending(x => x.Value);
            counter = 0;
            foreach(var skillsetDicitionaryEntry in sortedDictionary)
            {
                if(counter<4)
                {
                    toShowSkillsets.Add(skillsetDicitionaryEntry.Key);
                }
                counter++;
            }
            toShowSkillsets = toShowSkillsets.OrderByDescending(x => x.SkillsList.Count).ToList();
            return toShowSkillsets;
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

        private List<Experience> FormSortedListOfRelevantExperiences()
        {
            List<Employment> employmentsWithRelevantExperiences = new List<Employment>();
            List<Experience> relevantExperiences = new List<Experience>();
            int counter = 0;
            foreach(Employment employment in EmploymentsList)
            {
                employmentsWithRelevantExperiences.Add(new Employment(employment));
                employmentsWithRelevantExperiences.Last().ExperiencesList.Clear();
                foreach (Experience experience in employment.ExperiencesList)
                {
                    foreach (Skill relevantSkill in RelevantSkills)
                    {
                        if (experience.Tag.Equals(relevantSkill.SkillName))
                        {
                            employmentsWithRelevantExperiences.Last().ExperiencesList.Add(experience);
                            counter++;
                        }
                    }
                }
                List<Experience> sortedExperiences = employmentsWithRelevantExperiences.Last().ExperiencesList.OrderByDescending(x => (int)x.Priority).ToList();
                employmentsWithRelevantExperiences.Last().ExperiencesList.Clear();
                foreach(Experience experience in sortedExperiences)
                {
                    employmentsWithRelevantExperiences.Last().ExperiencesList.Add(experience);
                }
            }
            int pointer = 0;
            for(int i=0; i<=counter-1; i++)
            {
                //This is horrible! FIX ME!!!!
                while (true)
                {
                    if (employmentsWithRelevantExperiences[pointer].ExperiencesList.Count > 0)
                    {
                        relevantExperiences.Add(employmentsWithRelevantExperiences[pointer].ExperiencesList.First());
                        employmentsWithRelevantExperiences[pointer].ExperiencesList.RemoveAt(0);
                        break;
                    }
                    pointer++;
                    if (pointer > employmentsWithRelevantExperiences.Count - 1)
                    {
                        pointer = 0;
                    }
                }
                //Up to this point
                pointer++;
                if (pointer>employmentsWithRelevantExperiences.Count-1)
                {
                    pointer = 0;
                }
            }
            if (relevantExperiences.Count<10)
            {
                foreach (Employment employment in EmploymentsList)
                {
                    foreach (Experience experience in employment.ExperiencesList)
                    {
                        if (experience.Tag.Equals("Core"))
                        {
                            relevantExperiences.Add(experience);
                        }
                    }
                }
            }
            return relevantExperiences;
        }

        public void BuildResume(string pathToOutputGeneratedResume)
        {
            bool isExeedingOnePage;
            QuestPDF.Settings.License = LicenseType.Community;
            CultureInfo.CurrentCulture = App.Language;
            ResourceManager resourseManager = new ResourceManager(typeof(Properties.Resources));
            do
            {
                isExeedingOnePage = false;
                var resume = Document.Create(container =>
                {
                    _ = container.Page(page =>
                    {
                        //Page Settings
                        page.Size(PageSizes.A4);
                        page.Margin(0, Unit.Centimetre);
                        page.PageColor(Colors.White);
                        page.DefaultTextStyle(x => x.FontSize(8).FontFamily(Fonts.Calibri));

                        page.Content().Row(row =>
                        {
                            //Left Row
                            row.ConstantItem(240)
                            .Background("002256")
                            .ExtendVertical()
                            .Padding(10)
                            .ShowOnce()
                            .Column(column =>
                            {
                                //Avatar, Name and Title----------------------------------------------------------------------------------------------------------------------------------------------------
                                column.Item().Table(table =>
                                {
                                    table.ColumnsDefinition(columns =>
                                    {
                                        columns.RelativeColumn();
                                        columns.RelativeColumn();
                                    });
                                    //Avatar
                                    table.Cell().Row(1).Column(1).Element(LeftColumnMain).Image(Properties.Resources.Avatar2);
                                    //Name and Title
                                    table.Cell().Row(1).Column(2).Element(LeftColumnMain).Text(text =>
                                    {
                                        text.DefaultTextStyle(x => x.FontSize(20).FontColor(textColorCVLight));
                                        //text.Line(Name).ExtraBold().LineHeight(0.8f);
                                        //text.Line(Title).FontSize(18).LineHeight(0.8f);
                                    });
                                    table.Cell().Row(1).Column(2).AlignRight().AlignMiddle().Text(Name).FontSize(20).LineHeight(0.8f).FontColor(textColorCVLight);
                                    table.Cell().Row(2).Column(1).ColumnSpan(2).AlignMiddle().AlignLeft().Padding(5).Text(Title).FontSize(18).LineHeight(0.8f).FontColor(textColorCVLight);
                                });
                                //Summary---------------------------------------------------------------------------------------------------------------------------------------------------------------
                                column.Item().Table(table =>
                                {
                                    table.ColumnsDefinition(columns =>
                                    {
                                        columns.ConstantColumn(100);
                                        columns.RelativeColumn();
                                    });
                                    table.Cell().Row(1).Column(1).Element(LeftColumnMain).Text((string)App.Current.Resources["lang_Summary"]).FontColor(textColorCVLight).FontSize(16);
                                    table.Cell().Row(1).Column(2).AlignMiddle().PaddingVertical(15).LineHorizontal(1).LineColor(textColorCVLight);
                                });
                                column.Item().AlignLeft().Text(Summary)
                                .FontColor(textColorCVLight).FontSize(11).Light();
                                //Tools-----------------------------------------------------------------------------------------------------------------------------------------------------------------
                                column.Item().Table(table =>
                                {
                                    table.ColumnsDefinition(columns =>
                                    {
                                        columns.ConstantColumn(100);
                                        columns.RelativeColumn();
                                    });
                                    table.Cell().Row(1).Column(1).Element(LeftColumnMain).Text((string)App.Current.Resources["lang_Tools"]).FontColor(textColorCVLight).FontSize(16).LineHeight(0.6f);
                                    table.Cell().Row(1).Column(2).AlignMiddle().PaddingVertical(12).LineHorizontal(1).LineColor(textColorCVLight);
                                });
                                List<string> skillsNames;
                                foreach (Skillset toolsCategoty in GetToolsList(RelevantSkillsets))
                                {
                                    skillsNames = new List<string>();
                                    foreach (Skill skill in toolsCategoty.SkillsList.Reverse())
                                    {
                                        skillsNames.Add(skill.SkillName);
                                    }
                                    column.Item().Layers(layers =>
                                    {
                                        layers.Layer().Canvas((canvas, size) =>
                                        {
                                            DrawRoundedRectangle("#0C2C5E", false);
                                            //DrawRoundedRectangle(Colors.Blue.Darken2, true);
                                            void DrawRoundedRectangle(string color, bool isStroke)
                                            {
                                                using var paint = new SKPaint
                                                {
                                                    Color = SKColor.Parse(color),
                                                    IsStroke = isStroke,
                                                    StrokeWidth = 1,
                                                    IsAntialias = true
                                                };
                                                canvas.DrawRoundRect(0, 0, size.Width, size.Height, 14, 14, paint);
                                            }
                                        });

                                        layers.PrimaryLayer()
                                        .Table(table =>
                                        {
                                            table.ColumnsDefinition(columns =>
                                            {
                                                columns.RelativeColumn();
                                            });
                                            table.Cell().Row(1).Column(1).AlignCenter().Text(toolsCategoty.MainSkill).FontSize(14).FontColor(textColorCVLight).ExtraBold();
                                            table.Cell().Row(2).Column(1).AlignLeft().PaddingHorizontal(6).Text(String.Join(", ", skillsNames)).FontSize(11).FontColor(textColorCVLight);
                                            table.Cell().Row(3).Column(1).Text("").FontSize(2);
                                        });
                                    }); 
                                    column.Item().Text("").FontSize(5);
                                }
                                //Languages--------------------------------------------------------------------------------------------------------------------------------------------------------------
                                column.Item().Table(table =>
                                {
                                    table.ColumnsDefinition(columns =>
                                    {
                                        columns.ConstantColumn(100);
                                        columns.RelativeColumn();
                                    });
                                    table.Cell().Row(1).Column(1).Element(LeftColumnMain).Text((string)App.Current.Resources["lang_Languages"]).FontColor(textColorCVLight).FontSize(16).LineHeight(0.6f);
                                    table.Cell().Row(1).Column(2).AlignMiddle().PaddingVertical(12).LineHorizontal(1).LineColor(textColorCVLight);
                                });
                                foreach (Language language in LanguagesList)
                                {
                                    column.Item().Text(text =>
                                    {
                                        text.DefaultTextStyle(x => x.FontColor(textColorCVLight).FontSize(11));
                                        try
                                        {
                                            foreach (System.Collections.DictionaryEntry entry in resourseManager.GetResourceSet(System.Globalization.CultureInfo.CurrentCulture, true, true))
                                            {
                                                if (entry.Key.ToString().ToLower().Equals(language.LanguageName.ToLower()))
                                                {
                                                    text.Element().PaddingBottom(-6).Height(20).Image((byte[])entry.Value).FitHeight();
                                                }
                                            }
                                        }
                                        catch (Exception ex)
                                        {
                                            text.Span(language.LanguageName + " ");
                                        }
                                        text.Span("   " + App.Current.Resources["lang_Language_" + language.LanguageName] + " - " + language.Proficiency).LineHeight(0.6f);
                                    });
                                    column.Item().Text("").FontSize(10);
                                }
                                //Contacts-------------------------------------------------------------------------------------------------------------------------------------------------------------
                                column.Item().Table(table =>
                                {
                                    table.ColumnsDefinition(columns =>
                                    {
                                        columns.ConstantColumn(75);
                                        columns.RelativeColumn();
                                    });
                                    table.Cell().Row(1).Column(1).Element(LeftColumnMain).Text((string)App.Current.Resources["lang_Contacts"]).FontColor(textColorCVLight).FontSize(16).LineHeight(0.6f);
                                    table.Cell().Row(1).Column(2).AlignMiddle().PaddingVertical(12).LineHorizontal(1).LineColor(textColorCVLight);
                                });
                                foreach (Contact contactOption in ContactsList)
                                {
                                    column.Item().Text(text =>
                                    {
                                        text.DefaultTextStyle(x => x.FontColor(textColorCVLight).FontSize(11));
                                        try
                                        {
                                            foreach (System.Collections.DictionaryEntry entry in resourseManager.GetResourceSet(System.Globalization.CultureInfo.CurrentCulture, true, true))
                                            {
                                                if (entry.Key.ToString().ToLower().Equals(contactOption.ContactType.ToLower()))
                                                {
                                                    text.Element().PaddingBottom(-6).Height(16).Image((byte[])entry.Value).FitHeight();
                                                }
                                            }
                                        }
                                        catch (Exception ex)
                                        {
                                            text.Span(contactOption + " ");
                                        }
                                        text.Span("   " + contactOption.ContactDescription);
                                    });
                                    column.Item().Text("").FontSize(5);
                                }
                            });
                            //Right Row
                            row.RelativeItem()
                            .Padding(0)
                            .ShowEntire()
                            //Work Experience-------------------------------------------------------------------------------------------------------------------------------------------------------
                            .Column(column =>
                            {
                                column.Item().Table(table =>
                                {
                                    table.ColumnsDefinition(columns =>
                                    {
                                        columns.ConstantColumn(135);
                                        columns.RelativeColumn();
                                    });
                                    table.Cell().Row(1).Column(1).Element(LeftColumnMain).Text((string)App.Current.Resources["lang_Experience"]).FontColor(textCVAccentColor).FontSize(16).LineHeight(0.6f);
                                    table.Cell().Row(1).Column(2).AlignMiddle().PaddingVertical(12).LineHorizontal(1).LineColor(textCVAccentColor);
                                });
                                //Experiences
                                foreach (string employer in GetEmployersList(EmploymentsList))
                                {
                                    column.Item().AlignRight().PaddingHorizontal(10).Text(employer).FontColor(textCVAccentColor).FontSize(13).LineHeight(0.8f);
                                    column.Item().AlignRight().PaddingHorizontal(10)
                                    .Text(GetEmployerStartDate(EmploymentsList, employer).ToString("Y") + " - " + GetEmployerEndDate(EmploymentsList, employer).ToString("Y"))
                                    .FontColor("282828").FontSize(8).LineHeight(1f);
                                    foreach (Employment position in EmploymentsList)
                                    {
                                        if (position.Employer == employer)
                                        {
                                            column.Item().AlignLeft().PaddingHorizontal(5).Text(position.Title).FontColor(textCVAccentColor).FontSize(12).LineHeight(0.8f);
                                            column.Item().AlignLeft().PaddingHorizontal(5)
                                            .Text(position.StartDate.ToString("Y") + " - " + position.EndDate.ToString("Y"))
                                            .FontColor("282828").FontSize(9).LineHeight(1f);
                                            column.Item().AlignLeft().PaddingHorizontal(5).Text("").FontSize(4);
                                            foreach (Experience experience in position.ExperiencesList)
                                            {
                                                if (ExperiencesList.Contains(experience))
                                                {
                                                    column.Item().AlignLeft().PaddingHorizontal(15).Text("• " + experience.Description).FontSize(10).FontColor("000000").LineHeight(1f);
                                                }
                                            }
                                            column.Item().AlignLeft().PaddingHorizontal(5).Text("").FontSize(4);
                                        }
                                    }
                                }
                                //Education-------------------------------------------------------------------------------------------------------------------------------------------------------
                                column.Item().Table(table =>
                                {
                                    table.ColumnsDefinition(columns =>
                                    {
                                        columns.ConstantColumn(110);
                                        columns.RelativeColumn();
                                    });
                                    table.Cell().Row(1).Column(1).Element(LeftColumnMain).Text((string)App.Current.Resources["lang_Education"]).FontColor(textCVAccentColor).FontSize(16).LineHeight(0.6f);
                                    table.Cell().Row(1).Column(2).AlignMiddle().PaddingVertical(12).LineHorizontal(1).LineColor(textCVAccentColor);
                                });
                                foreach (Education education in GetEducationsList(EducationsList))
                                {
                                    column.Item().AlignRight().PaddingHorizontal(10).Text(education.Institution).FontColor(textCVAccentColor).FontSize(13).LineHeight(0.8f);
                                    column.Item().AlignRight().PaddingHorizontal(10).Text(education.StartDate.ToString("Y", App.Language) + " - " +
                                        education.EndDate.ToString("Y", App.Language)).FontColor("282828").FontSize(9).LineHeight(1f);
                                    column.Item().AlignLeft().PaddingHorizontal(5).Text(text =>
                                    {
                                        text.DefaultTextStyle(x => x.FontSize(12).FontColor(textCVAccentColor));
                                        text.Span(education.Degree).ExtraBold();
                                        text.Span(" - " + education.Program);
                                    });
                                    column.Item().AlignLeft().PaddingHorizontal(5).Text(text =>
                                    {
                                        text.DefaultTextStyle(x => x.FontSize(9).FontColor("282828"));
                                        text.Span(education.StartDate.ToString("Y", App.Language) + " - " + education.EndDate.ToString("Y", App.Language));
                                        if (education.WithHonors)
                                        {
                                            text.Span(" - " + (string)App.Current.Resources["lang_Education_WithHonors_Show"]).FontColor("FF0000");
                                        }
                                    });
                                    column.Item().AlignLeft().PaddingHorizontal(5).Text(education.Description).FontColor("000000").FontSize(10);
                                    column.Item().Text("").FontSize(2);
                                }
                                // Professional Affiliations------------------------------------------------------------------------------------------------------------------------------------
                                column.Item().Table(table =>
                                {
                                    table.ColumnsDefinition(columns =>
                                    {
                                        columns.ConstantColumn(190);
                                        columns.RelativeColumn();
                                    });
                                    table.Cell().Row(1).Column(1).Element(LeftColumnMain).Text((string)App.Current.Resources["lang_Affiliations"]).FontColor(textCVAccentColor).FontSize(16).LineHeight(0.6f);
                                    table.Cell().Row(1).Column(2).AlignMiddle().PaddingVertical(12).LineHorizontal(1).LineColor(textCVAccentColor);
                                });
                                foreach (ProffessionalAffiliation affiliation in AffiliationsList)
                                {
                                    column.Item().PaddingHorizontal(5).Text(affiliation.ToString()).FontSize(10).FontColor("000000").LineHeight(1f);
                                }
                            });
                        });
                    });
                });
                try
                {
                    resume.GeneratePdf(pathToOutputGeneratedResume);
                }
                catch (Exception ex)
                {
                    if (ex is DocumentLayoutException)
                    {
                        isExeedingOnePage = true;
                        ExperiencesList.RemoveAt(ExperiencesList.Count - 1);
                    }
                }
            } while (isExeedingOnePage);
            new Process { StartInfo = new ProcessStartInfo(pathToOutputGeneratedResume) { UseShellExecute = true } }.Start();
            //resume.ShowInPreviewer();
        }
    }
}
