using Autodesk.Revit.DB;

namespace RevitSqlShared
{
	public static class AutoRunner
	{
		public static void Run(Document doc)
		{
			SqlExporter.ExportDoors(doc);
		}
	}
}
