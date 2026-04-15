using Autodesk.Revit.DB;
using Autodesk.Revit.ApplicationServices;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace RevitSqlShared
{
	public static class SqlExporter
	{
		private const string PARAM_GROUP = "AH SQL Integration";
		private const string PARAM_NAME = "AH_CompanyCode";

		public static void ExportDoors(Document doc)
		{
			Application app = doc.Application;

			Guid paramGuid = SharedParamUtils.GetGuid(
					app,
					PARAM_GROUP,
					PARAM_NAME
					);

			if (paramGuid == Guid.Empty)
				return;

			IList<Element> doors =
				new FilteredElementCollector(doc)
				.OfCategory(BuiltInCategory.OST_Doors)
				.WhereElementIsNotElementType()
				.ToElements();

			using (SqlConnection conn =
					new SqlConnection(SqlConfig.ConnectionString))
			{
				conn.Open();

				foreach (Element door in doors)
				{
					Parameter p = door.get_Parameter(paramGuid);
					string value = p != null ? p.AsString() : "";

					if (value == null)
						value = "";

					UpsertDoor(
							conn,
							door.UniqueId,
							door.Name,
							value
						  );
				}
			}
		}

		private static void UpsertDoor(
				SqlConnection conn,
				string uniqueId,
				string familyType,
				string companyCode)
		{
			string sql =
				@"MERGE AH_Doors AS target
				USING (SELECT @uid AS UniqueId) AS source
				ON target.UniqueId = source.UniqueId
				WHEN MATCHED THEN
				UPDATE SET
				FamilyType = @ft,
			AH_CompanyCode = @cc,
			LastSync = GETDATE()
				WHEN NOT MATCHED THEN
				INSERT (UniqueId, FamilyType, AH_CompanyCode)
				VALUES (@uid, @ft, @cc);";

			using (SqlCommand cmd = new SqlCommand(sql, conn))
			{
				cmd.Parameters.AddWithValue("@uid", uniqueId);
				cmd.Parameters.AddWithValue("@ft", familyType);
				cmd.Parameters.AddWithValue("@cc", companyCode);
				cmd.ExecuteNonQuery();
			}
		}
	}
}
