using HedgehogDevelopment.SitecoreProject.Tasks.ProjectAnalysis;
using System;
using System.Collections.Generic;
using System.Linq;

namespace TDSCustomValidators.ContentValidators
{
    [Validator("TDSCC001", Status.Warn, Description = "Content Items Under The Home Node Must Have Presentation Details",
        Name = "Content Under Home Must Have Presentation Details")]
    public class ContentUnderHomeMustHavePresentationDetails: UserConfigurableValidator
    {
        public override ValidatorSettings GetDefaultSettings()
        {
            return new ValidatorSettings
            {
                Properties = new List<string>
                {
                    "/sitecore/content/home",
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

                    if (string.IsNullOrEmpty(item.Value.ParsedItem.Fields["__Renderings"]))
                {
                    ProblemLocation position = GetItemPosition(scprojDocument, item.Value.Item);

                    Problem report = new Problem(this, position)
                    {
                        Message = string.Format("Content Item Under Home Doesn't Have Presentation Details {0}", item.Value.ParsedItem.Path)
                    };

                    yield return report;
                }
                    
                }
            }
        }
    }
}


