using Autodesk.Revit.UI;
using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Events;

namespace RevitSqlShared
{
	public class App : IExternalApplication
	{
		public Result OnStartup(UIControlledApplication app)
		{
			app.ControlledApplication.DocumentOpened += OnDocumentOpened;
			return Result.Succeeded;
		}

		public Result OnShutdown(UIControlledApplication app)
		{
			app.ControlledApplication.DocumentOpened -= OnDocumentOpened;
			return Result.Succeeded;
		}

		private void OnDocumentOpened(object sender, DocumentOpenedEventArgs e)
		{
			Document doc = e.Document;

			if (doc == null || doc.IsFamilyDocument)
				return;

			AutoRunner.Run(doc);
		}
	}
}


