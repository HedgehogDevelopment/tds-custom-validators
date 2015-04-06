using HedgehogDevelopment.SitecoreProject.Tasks.ProjectAnalysis;
using System;
using System.Collections.Generic;
using System.Linq;

namespace TDSCustomValidators.ContentValidators
{
    [Validator("TDSCC003", Status.Warn, Description = "Item With More Than 30 Children Must Be Bucket",
        Name = "Item With More than 30 Children Must Be Buckets")]
    public class ItemMustUseBucket : UserConfigurableValidator
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
                    x => item.Value.Item.SitecoreItemPath.StartsWith(x, StringComparison.InvariantCultureIgnoreCase))){

                        if (item.Value.ParsedItem.Children.Count > 30 && item.Value.ParsedItem.Fields["__Is Bucket"] != "1")
                    {
                        ProblemLocation position = GetItemPosition(scprojDocument, item.Value.Item);

                        Problem report = new Problem(this, position)
                        {
                            Message = string.Format("Item Must Be Bucket {0}", item.Value.ParsedItem.Path)
                        };

                        yield return report;
                    }
                }

            }
        }
    }
}
