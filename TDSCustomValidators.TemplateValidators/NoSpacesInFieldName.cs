using HedgehogDevelopment.SitecoreProject.Tasks.ProjectAnalysis;
using System;
using System.Collections.Generic;
using System.Linq;

namespace TDSCustomValidators.TemplateValidators
{

    [Validator("TDSCT002", Status.Warn, Description = "Template Field Names must not have spaces in them",
        Name = "No Spaces In Template Fields")]
    public class NoSpacesInFieldName : UserConfigurableValidator
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
                    if (item.Value.Item.TemplateId == "{455A3E98-A627-4B40-8035-E683A0331AC7}")
                    {
                        if (!string.IsNullOrEmpty(item.Value.ParsedItem.Name) && item.Value.ParsedItem.Name.Contains(" "))
                        {
                            ProblemLocation position = GetItemPosition(scprojDocument, item.Value.Item);

                            Problem report = new Problem(this, position)
                            {
                                Message = string.Format("Template Field Name Contains Space {0}", item.Value.ParsedItem.Path)
                            };

                            yield return report;
                        }
                    }
                }
            }
        }
    }
}