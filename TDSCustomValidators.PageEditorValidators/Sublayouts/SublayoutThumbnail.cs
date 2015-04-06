using HedgehogDevelopment.SitecoreProject.Tasks.ProjectAnalysis;
using System;
using System.Collections.Generic;
using System.Linq;

namespace TDSCustomValidators.PageEditorValidators.Sublayouts
{
    [ValidatorAttribute("TDSCPE001", Status.Warn, Description = "Sublayouts Must Have Thumbnail Set",
        Name = "Sublayout Thumbnail")]
    public class SublayoutThumbnail : UserConfigurableValidator
    {
        public override ValidatorSettings GetDefaultSettings()
        {
            return new ValidatorSettings
            {
                Properties = new List<string>
                {
                    "/sitecore/layout",
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
                    if (item.Value.Item.TemplateId == "{0A98E368-CDB9-4E1E-927C-8E0C24A003FB}")
                    {
                        if (string.IsNullOrEmpty(item.Value.ParsedItem.Fields["__Thumbnail"]))
                        {
                            ProblemLocation position = GetItemPosition(scprojDocument, item.Value.Item);

                            Problem report = new Problem(this, position)
                            {
                                Message = string.Format("Sublayout Doesn't Have Thumbnail Set {0}", item.Value.ParsedItem.Path)
                            };

                            yield return report;
                        }
                    }
                }
            }
        }
    }
}
