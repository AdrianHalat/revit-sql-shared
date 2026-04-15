# RevitSqlShared

`RevitSqlShared` is a lightweight **Revit Application Add-In** that runs **automatically** when Revit starts and reacts to **document lifecycle events** (currently: when a project model is opened).

Its primary purpose is **one-way background synchronization of Revit model data to SQL Server**, without any UI, commands, or user interaction.

The add-in is designed as a **shared infrastructure layer** for automation, integrations, logging, and data export.

---

## What This Add-In Does

- Automatically runs when Revit starts
- Listens for project open events
- Reads shared parameters from model elements
- Exports data **from Revit to SQL Server**
- Updates existing SQL records safely (no duplicates)

---

## What This Add-In Does NOT Do

- No Ribbon buttons
- No external commands
- No UI dialogs
- No SQL → Revit import
- No deletion of SQL records when elements are removed
- No physical delete of data in SQL

SQL is treated as a **read-only mirror** of the current Revit model state.

---

## Synchronization Model

This add-in implements a **one-way synchronization**:

Revit model → SQL Server

- Revit is the single source of truth
- SQL data is inserted or updated using Revit `UniqueId`
- Running the add-in multiple times is safe

---

## When Does It Run?

1. Revit starts
2. The `.addin` manifest is loaded
3. The DLL is loaded into memory
4. `App.OnStartup()` is executed
5. The add-in subscribes to `DocumentOpened`
6. Each time a project model is opened, data is synchronized

---

## What Is Synchronized

- Category: Doors
- Key: Revit `UniqueId`
- Fields:
  - UniqueId
  - FamilyType
  - AH_CompanyCode
  - LastSync

Each element corresponds to exactly one SQL row.

---

## Project Structure

```
revit-sql-shared
│
├─ dist/
├─ libs/
├─ manifest/
├─ src/
├─ test-env/
│
├─ build.cmd
├─ run.cmd
└─ Readme.md
```

---

## Build System

The project is compiled using the .NET Framework C# compiler:

C:\Windows\Microsoft.NET\Framework64\v4.0.30319\csc.exe

No Visual Studio or MSBuild is required.

### Build

```
build.cmd
```

---

## Deployment Location

Revit loads add-ins exclusively from:

C:\ProgramData\Autodesk\Revit\Addins\2024

Only `.addin` and `.dll` files placed in this directory are loaded.

---

## Shared Parameters Requirements

- A shared parameters file must be selected in Revit
- A shared parameter must exist:
  - Group: AH SQL Integration
  - Name: AH_CompanyCode
  - Type: Text
- The parameter must be bound as an Instance parameter to Doors

The add-in does not create or bind parameters.

---

## SQL Requirements

- SQL Server instance accessible via Integrated Security
- Database: Revit_AH_Test
- Table: AH_Doors
- Primary key: UniqueId

---

## Test & Run

```
run.cmd
```

This copies the add-in files, launches Revit, and opens the test model from `test-env`.

---

## Supported Environment

- Revit 2024
- Windows x64
- .NET Framework 4.x

---

## Intended Use

This repository is intended as a stable internal foundation for background automation, model observation, and SQL-based analytics pipelines.
