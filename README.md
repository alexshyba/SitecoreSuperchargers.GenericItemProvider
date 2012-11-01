<h1>SitecoreSuperchargers.GenericItemProvider</h1>
A Supercharger for Sitecore CMS that allows building data integration with ease.
<h2>About</h2>
The purpose of GIP is inbound, unidirectional process of bringing external data, materializing (creating) it in Sitecore and refreshing it periodically.
It does not support the process of updating external repository from Sitecore, and in 99% of scenarios, this is not what the customers want anyways. 
Sitecore is usually placed as a content aggregation hub, not an alternative UI to update somebody else’s data.
You can, in fact, add a triggering event to update external source (via item:saved, a workflow action, etc.), but it is not out of the box.
Also, GIP can be used for the import, as it creates items. Once the initial load is done, you can simply remove the micro-pipeline from the config, and the items will stay there in Sitecore.
<h2>HOW TO BUILD</h2>
To build, you will need to copy Sitecore.Kernel.dll and Sitecore.Logging.dll into the sc.lib folder.
TDS is used for this solution in order to facilitate the build & deploy, but not a requirement.