using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Fluidity;
using Fluidity.Configuration;
using Website.Sample.DbModels;

namespace Website.Sample.Events
{
    public class FluidityBootstrap : FluidityConfigModule
    {
        public override void Configure(FluidityConfig config)
        {
            config.AddSection("Database", "icon-server-alt", sectionConfig => {

                sectionConfig.SetTree("Database", treeConfig => {

                    treeConfig.AddCollection<Person>(p => p.Id, "Person", "People", "A collection of people", "icon-umb-users", "icon-umb-users", collectionConfig => {

                        collectionConfig.SetNameProperty(p => p.Name);
                        collectionConfig.SetViewMode(FluidityViewMode.List);

                        collectionConfig.ListView(listViewConfig => {
                            listViewConfig.AddField(p => p.JobTitle);
                            listViewConfig.AddField(p => p.Email);
                        });

                        collectionConfig.Editor(editorConfig => {

                            editorConfig.AddTab("General", tabConfig => {
                                tabConfig.AddField(p => p.JobTitle).MakeRequired();
                                tabConfig.AddField(p => p.Email).SetValidationRegex("[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+.[a-zA-Z0-9-.]+");
                                tabConfig.AddField(p => p.Telephone).SetDescription("inc area code");
                                tabConfig.AddField(p => p.Age);
                            });

                            editorConfig.AddTab("Media", tabConfig => {
                                tabConfig.AddField(p => p.Avatar).SetDataType("Upload");
                            });

                        });

                    });

                });

            });
        }
    }

}
