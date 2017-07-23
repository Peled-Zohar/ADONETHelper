
ADONETHelper

ADONETHelper simplifies the code working with ADO.Net by encapsulating connection, command, parameters and data adapters. It implements the abstract factory desing pattern so the user code is the same for OleDb, Odbc and Sql.

This repository contains two projects: ADONETHelper and HowToUseADONETHelper. The second one contains some code samples of working with ADONETHelper, including initialization, executing sql statements, using parameters ect`.

Please note that ADONETHelper does not contain any exception handling code, so you should still use try...catch when calling the execute and fill functions.
