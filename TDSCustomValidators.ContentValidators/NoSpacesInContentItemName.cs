using HedgehogDevelopment.SitecoreProject.Tasks.ProjectAnalysis;
using System;
using System.Collections.Generic;
using System.Linq;

namespace TDSCustomValidators.ContentValidators
{
     [Validator("TDSCC002", Status.Warn, Description = "Content Item Name must not have spaces",
        Name = "No Spaces In Content Item Name")]
    public class NoSpacesInContentItemName : UserConfigurableValidator
    {
        public override ValidatorSettings GetDefaultSettings()
        {
            return new ValidatorSettings
            {
                Properties = new List<string>
                {
                    "/sitecore/content",
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

                    if (!string.IsNullOrEmpty(item.Value.ParsedItem.Name) && item.Value.ParsedItem.Name.Contains(" "))
                    {
                        ProblemLocation position = GetItemPosition(scprojDocument, item.Value.Item);

                        Problem report = new Problem(this, position)
                        {
                            Message = string.Format("Content Item Name Contains Space {0}", item.Value.ParsedItem.Path)
                        };

                        yield return report;
                    }
                }

            }
        }
    }
}