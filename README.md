AzureVmFarmer
=============

A service based approach for programmatically creating and deleting virtual machines in an azure service.

This is a service that you call to create an Azure VM from an arbitrary image, and can optionally attach drives. You can also use the service to delete the image and all its artifacts (service endpoint, vm specific disks, etc). There are two pieces a web service, which is just a REST endpoint for entry into the system and a worker role that does the heavy lifting.

If this looks like something you want to implement, I would strongly recommend finding another way to accomplish your goal. I needed this for testing a specific architecture, but am also working to improve that architecture so that this testing setup is no longer necessary.

I am assuming you have the following:

* Azure Account
* Prepped Image uploaded to Azure
* Any needed data disks in a storage account

**Config Values**
