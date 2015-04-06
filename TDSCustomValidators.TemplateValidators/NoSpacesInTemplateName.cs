using HedgehogDevelopment.SitecoreProject.Tasks.ProjectAnalysis;
using System;
using System.Collections.Generic;
using System.Linq;

namespace TDSCustomValidators.TemplateValidators
{
   [Validator("TDSCT004", Status.Warn, Description = "Template Name must not have spaces",
        Name = "No Spaces In Template Name")]
    public class NoSpacesInTemplateName : UserConfigurableValidator
    {
        public override ValidatorSettings GetDefaultSettings()
        {
            return new ValidatorSettings
            {
                Properties = new List<string>
                {
                    "/sitecore/templates",
                }
            };
        }

        public override IEnumerable<Problem> Validate(
            Dictionary<Guid, HedgehogDevelopment.SitecoreProject.Tasks.SitecoreDeployInfo> projectItems,
            System.Xml.Linq.XDocument scprojDocument)
        {
            foreach (var item in projectItems)
            {
                if (Settings.Properties.Any(
                    x => item.Value.Item.SitecoreItemPath.StartsWith(x, StringComparison.InvariantCultureIgnoreCase)))
                {
                    if (item.Value.Item.TemplateId == "{AB86861A-6030-46C5-B394-E8F99E8B87DB}")
                    {
                        if (!string.IsNullOrEmpty(item.Value.ParsedItem.Name) && item.Value.ParsedItem.Name.Contains(" "))
                        {
                            ProblemLocation position = GetItemPosition(scprojDocument, item.Value.Item);

                            Problem report = new Problem(this, position)
                            {
                                Message = string.Format("Template Name Contains Space {0}", item.Value.ParsedItem.Path)
                            };

                            yield return report;
                        }
                    }
                }
            }
        }
    }
}