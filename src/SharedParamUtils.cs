using Autodesk.Revit.ApplicationServices;
using Autodesk.Revit.DB;
using System;

namespace RevitSqlShared
{
	public static class SharedParamUtils
	{
		public static Guid GetGuid(
				Application app,
				string groupName,
				string paramName)
		{
			DefinitionFile file = app.OpenSharedParameterFile();
			if (file == null)
				return Guid.Empty;

			DefinitionGroup group = file.Groups.get_Item(groupName);
			if (group == null)
				return Guid.Empty;

			Definition def = group.Definitions.get_Item(paramName);
			if (def == null)
				return Guid.Empty;

			ExternalDefinition extDef = def as ExternalDefinition;
			if (extDef == null)
				return Guid.Empty;

			return extDef.GUID;
		}
	}
}
