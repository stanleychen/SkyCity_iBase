using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Configuration;
using Microsoft.ApplicationBlocks.Data;
using System.Collections.Specialized;
using System.Data.Sql;
using System.Data.SqlClient;
using System.IO;
using System.Threading;
using Microsoft.SqlServer.Dts.Runtime;
using dts = Microsoft.SqlServer.Dts.Runtime.Wrapper;

using Microsoft.SqlServer.Dts.Pipeline;
using Microsoft.SqlServer.Dts.Pipeline.Wrapper;

using iTrak.Importer.Data;
using iTrak.Importer.Data.Importers;
using iTrak.Importer.Entities;
using iTrak.Importer.Common;

namespace iTrak.ImporterMain
{
    public partial class Form1 : Form
    {
        #region Properties
        public string iXDataConnectionString
        {
            get
            {
                string connString = string.Empty;
                if (ConfigurationManager.ConnectionStrings["iTrak"] != null)
                    connString = ConfigurationManager.ConnectionStrings["iTrak"].ToString();
                else
                    connString = iView.iTrak.iTrakCommon.Database.ConnectionString;

                return connString;
            }
        }
        #endregion

        public enum DataSourceType
        {
            NONE = 0,
            SQL = 1,
            Access = 2,
            Text = 3
        }
        private static DataTable _statTable = null;
        private static ManualResetEvent[] _resetEvents;
        private long _totalImportedRow = 0;
        private string _appPath = string.Empty;
        public Form1()
        {
            InitializeComponent();
            _appPath = Application.StartupPath;
        }

        #region Event Handler
        private void Form1_Load(object sender, EventArgs e)
        {
            CCGeneral.Logging.LogToDefault();

            this.label_TotalRows.Text = "";
            this.BuildInfoGrid();
        }

        
        private void button_CreateTempTable_Click(object sender, EventArgs e)
        {
            try
            {
               
                this.listBox_Status.Items.Add("Step 1 completed successfully");
            }
            catch (Exception ex)
            {
                CCGeneral.Logging.LogError("Failed to Create Temp Table", ex);
                MessageBox.Show(ex.Message);
            }
        }
        private void button_PrepareSubject_Click(object sender, EventArgs e)
        {
            try
            {
               this.listBox_Status.Items.Add("Step 3 completed successfully");
            }
            catch (Exception ex)
            {
                CCGeneral.Logging.LogError("Failed to prepare subject data", ex);
                MessageBox.Show(ex.Message);
            }
        }
     
        private void button_ImportMedia_Click(object sender, EventArgs e)
        {
            try
            {
                this.listBox_Status.Items.Add("Step 6 completed successfully");
            }
            catch (Exception ex)
            {
                CCGeneral.Logging.LogError("Failed to import media data", ex);
                MessageBox.Show(ex.Message);
            }

        }
   
        #endregion

        #region Import Data

        #region Import Incident
        private void ImportedIncidentRow_Handler(object sender, EventArgs e)
        {
            this.progressBar1.Value = (int)sender;
            this.label_Message.Text = "Importing Incident... Count: " + (int)sender;
            Application.DoEvents();
        }
        private void ImportIncident()
        {
            //iTrak.Importer.Data.IncidentImporter importer = new iTrak.Importer.Data.IncidentImporter(this.iXDataConnectionString, this.iXDataConnectionString);
            //this.progressBar1.Maximum = importer.GetSourceRowCount();
            //importer.ImportedRowNumber += new System.EventHandler(this.ImportedIncidentRow_Handler);
            //importer.ImportIncident();
            //this.progressBar1.Value = 0;
           
        }
        #endregion

        private void ImportedDataEvent_Handler(object sender, EventArgs e)
        {
            this.progressBar1.Value = (int)sender;
            ImportEventArgs eventArgs = e as ImportEventArgs;
            this.label_Message.Text = eventArgs.Message + "... Row: " + (int)sender + " of " + this.progressBar1.Maximum;
            Application.DoEvents();
        }

        private void ImportCompletedEvent_Handler(object sender, EventArgs e)
        {
            int importedCount = (int)sender;
            if (importedCount > 0)
            {
                DataRow row = _statTable.NewRow();
                row["Count"] = (int)sender;
                ImportEventArgs eventArgs = e as ImportEventArgs;
                row["Info"] = eventArgs.Message;
                _statTable.Rows.Add(row);

                _totalImportedRow += (int)sender;
                this.label_TotalRows.Text = "Total Imported Rows: " + _totalImportedRow.ToString();

                Application.DoEvents();
            }
        }

        private void ImportParticipantAssignments()
        {
            iTrak.Importer.Data.Importers.ParticipantAssignment importer = new iTrak.Importer.Data.Importers.ParticipantAssignment();
            this.progressBar1.Maximum = importer.Count;
            importer.ImportedRowEvent += new System.EventHandler(this.ImportedDataEvent_Handler);
            importer.ImportParticpantAssigments();
            this.progressBar1.Value = 0;
        }

        private void ImportVehicles()
        {
            iTrak.Importer.Data.Importers.Vehicle importer = new iTrak.Importer.Data.Importers.Vehicle();
            this.progressBar1.Maximum = importer.Count;
            importer.ImportedRowEvent += new System.EventHandler(this.ImportedDataEvent_Handler);
            importer.ImportVehicles();
            this.progressBar1.Value = 0;
        }
        #endregion

        #region Import Data & Import Button Event Handler
        
        #region Import Media

        private void ImportedFile_handler(object sender, EventArgs e)
        {
            this.progressBar1.Value = (int)sender;
            this.label_Message.Text = "Importing media... Count: " + (int)sender;
            Application.DoEvents();
        }
  
        #endregion


        #region Helper

        private void button_PrepareMedia_Click(object sender, EventArgs e)
        {
            try
            {
                this.listBox_Status.Items.Add("Step 5 completed successfully");
            }
            catch (Exception ex)
            {
                CCGeneral.Logging.LogError(ex.Message);
            }
        }

        #endregion


        #region Button Event Handlers

        #region button_ImportData_Click
        private void DeleteImportVehicles()
        {
            SqlConnection conn = null;
            SqlTransaction trans = null;
            try
            {
                conn = new SqlConnection(DbHelper.iTrakConnectionString);
                conn.Open();
                trans = conn.BeginTransaction();
                string sqlText = "delete from Vehicle WHERE VehicleGuid IN (select ConvertGuid from _ConversionTmp where tableName='Vehicle')";
                SqlHelper.ExecuteNonQuery(trans, CommandType.Text, sqlText);
                sqlText = "delete from _ConversionTmp where tableName='Vehicle'";
                SqlHelper.ExecuteNonQuery(trans, CommandType.Text, sqlText);
                trans.Commit();
            }
            catch (Exception ex)
            {
                trans.Rollback();
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                if (conn != null)
                    conn.Close();
            }
        }
 
        #endregion

        #endregion

        #endregion

        #region RunPackages
        private string GetiTrakPkgConnectionString()
        {

            string tempString = iXDataConnectionString;
            string[] connectionStrings = tempString.Split(';');
            string dataSource = string.Empty;
            string initialCatalog = string.Empty;
            string userID = string.Empty;
            string pwd = string.Empty;

            for (int i = 0; i < connectionStrings.Length; i++)
            {
                if (connectionStrings[i].ToUpper().Trim().StartsWith("DATA SOURCE"))
                {
                    dataSource = connectionStrings[i];
                }
                if (connectionStrings[i].ToUpper().Trim().StartsWith("INITIAL CATALOG"))
                {
                    initialCatalog = connectionStrings[i];
                }
                if (connectionStrings[i].ToUpper().Trim().StartsWith("UID") || connectionStrings[i].ToUpper().Trim().StartsWith("User ID"))
                {
                    userID = connectionStrings[i];
                }
                if (connectionStrings[i].ToUpper().Trim().StartsWith("PWD"))
                {
                    pwd = connectionStrings[i];
                }
            }
            string connectionString = string.Empty;
            if (userID.Length > 0) //SQL Login
            {
                connectionString = string.Format(@"{0};{1};{2};{3};Provider={4};Auto Translate=False;", dataSource, userID, pwd, initialCatalog, GetSqlProvider());
            }
            else
            {
                connectionString = string.Format("{0};{1};Provider={2};Integrated Security=SSPI;Auto Translate=False;", dataSource, initialCatalog, GetSqlProvider());
            }

            return connectionString;
        }
        private string GetSqlProvider()
        {
            string provider = "sqlncli.1";
            if (ConfigurationManager.AppSettings["SqlProvider"] != null)
            {
                provider = ConfigurationManager.AppSettings["SqlProvider"].ToString();
            }
            return provider;
        }
        private string GetPkgSQLConnnectionString(string dataKey)
        {
            string tempString = ConfigurationManager.ConnectionStrings[dataKey].ToString();
            string[] connectionStrings = tempString.Split(';');
            string dataSource = string.Empty;
            string initialCatalog = string.Empty;
            string userID = string.Empty;
            string pwd = string.Empty;

            for (int i = 0; i < connectionStrings.Length; i++)
            {
                if (connectionStrings[i].ToUpper().Trim().StartsWith("DATA SOURCE"))
                {
                    dataSource = connectionStrings[i];
                }
                if (connectionStrings[i].ToUpper().Trim().StartsWith("INITIAL CATALOG"))
                {
                    initialCatalog = connectionStrings[i];
                }
                if (connectionStrings[i].ToUpper().Trim().StartsWith("UID") || connectionStrings[i].ToUpper().Trim().StartsWith("User ID"))
                {
                    userID = connectionStrings[i];
                }
                if (connectionStrings[i].ToUpper().Trim().StartsWith("PWD"))
                {
                    pwd = connectionStrings[i];
                }
            }
            string connectionString = string.Empty;
            if (userID.Length > 0) //SQL Login
            {
               connectionString = string.Format(@"{0};{1};{2};{3};Provider={4};Auto Translate=False;", dataSource, userID, pwd, initialCatalog,GetSqlProvider());
            }
            else
            {
               connectionString = string.Format("{0};{1};Provider={2};Integrated Security=SSPI;Auto Translate=False;", dataSource, initialCatalog,GetSqlProvider());
            }

            return connectionString;

        }
        private string GetPkgAccessConnectionString(string dataKey)
        {
            string tempString = ConfigurationManager.AppSettings[dataKey];
            return string.Format("Data Source={0};Provider=Microsoft.Jet.OLEDB.4.0;",tempString);
        }
        private string GetPkgTextConnectionString(string appKey, string fileName)
        {
            string connString = string.Empty;
            string sourceLocation = string.Empty;
            if (ConfigurationManager.AppSettings[appKey] != null)
                sourceLocation = ConfigurationManager.AppSettings[appKey].ToString();

            if (!sourceLocation.EndsWith(@"\"))
                sourceLocation += @"\";

            return string.Format("{0}{1}", sourceLocation, fileName);
        }
        private void RunPackage(string sourceConnectionName, string packageName, DataSourceType sourceType, string fileName)
        {
            try
            {

                dts.Package pkg;
                dts.Application app;
                string pkgLocation = Path.Combine(Application.StartupPath, packageName);

                app = new dts.Application();
                pkg = (dts.Package)app.LoadPackage(packageName, true, null);

                if (!string.IsNullOrEmpty(sourceConnectionName))
                {
                    string sourceConnectionString = string.Empty;
                    if (sourceType == DataSourceType.Access)
                        sourceConnectionString = GetPkgAccessConnectionString(sourceConnectionName);
                    else if (sourceType == DataSourceType.Text)
                        sourceConnectionString = GetPkgTextConnectionString(sourceConnectionName, fileName);
                    else
                        sourceConnectionString = GetPkgSQLConnnectionString(sourceConnectionName);

                    pkg.Connections[sourceConnectionName].ConnectionString = sourceConnectionString;
                }
                pkg.Connections["iXData"].ConnectionString = GetiTrakPkgConnectionString();

                if (pkg.Execute() == Microsoft.SqlServer.Dts.Runtime.Wrapper.DTSExecResult.DTSER_FAILURE)
                {
                    StringBuilder sb = new StringBuilder();
                    for (int i = 0; i < pkg.Errors.Count; i++)
                    {
                        sb.Append(pkg.Errors[i].Description + System.Environment.NewLine);
                    }
                    throw new Exception(sb.ToString());
                }

            }
            catch (Exception ex)
            {
                CCGeneral.Logging.LogError(ex.Message);
                throw new Exception("Failed to run " + packageName, ex);
            }

        }

        private void RunPackage(string sourceConnectionName, string packageName, DataSourceType sourceType)
        {
            try
            {

                dts.Package pkg;
                dts.Application app;
                string pkgLocation = Path.Combine(Application.StartupPath,packageName);

                app = new dts.Application();
                pkg = (dts.Package)app.LoadPackage(packageName, true, null);
                
                if (!string.IsNullOrEmpty(sourceConnectionName))
                {
                    string sourceConnectionString = string.Empty;
                    if(sourceType == DataSourceType.Access)
                        sourceConnectionString = GetPkgAccessConnectionString(sourceConnectionName);
                    else
                        sourceConnectionString = GetPkgSQLConnnectionString(sourceConnectionName);

                    pkg.Connections[sourceConnectionName].ConnectionString = sourceConnectionString;
                }
                pkg.Connections["iXData"].ConnectionString =  GetiTrakPkgConnectionString();
               
                if (pkg.Execute() == Microsoft.SqlServer.Dts.Runtime.Wrapper.DTSExecResult.DTSER_FAILURE)
                {
                    StringBuilder sb = new StringBuilder();
                    for (int i = 0; i < pkg.Errors.Count; i++)
                    {
                        sb.Append(pkg.Errors[i].Description + System.Environment.NewLine);
                    }
                    throw new Exception(sb.ToString());
                }
                
            }
            catch (Exception ex)
            {
                CCGeneral.Logging.LogError(ex.Message);
                throw new Exception("Failed to run " + packageName, ex);
            }
          
        }
        #endregion

        #region Import Media

        #region Prepare Media
        private string GetSQLSourceDataConnectionString(string dataKey)
        {
            string tempString = ConfigurationManager.ConnectionStrings[dataKey].ConnectionString;
            string[] connectionStrings = tempString.Split(';'); 
            string dataSource = string.Empty;
            string initialCatalog = string.Empty;
            for (int i = 0; i < connectionStrings.Length; i++)
            {
                if (connectionStrings[i].ToUpper().IndexOf("DATA SOURCE=") > -1)
                {
                    dataSource = connectionStrings[i].ToUpper();
                }
                if (connectionStrings[i].ToUpper().IndexOf("INITIAL CATALOG=") > -1)
                {
                    initialCatalog = connectionStrings[i].ToUpper();
                }

            }
            return string.Format("{0};{1};Integrated Security=True", dataSource, initialCatalog);
        }
        #region StoreInTempLocation
        private static void StoreInTempLocation(object o)
        {
            object[] objs = o as object[];
            int index = (int)objs[0];
            string fileFullName = (string)objs[1];
            object blob = objs[2];
            FileStream fs = null;
            try
            {
                fs = new FileStream(fileFullName, FileMode.Create);
                byte[] bytes = (byte[])blob;
                fs.Write(bytes, 0, bytes.Length);
            }
            catch (Exception ex)
            {
                CCGeneral.Logging.LogError("Failed to save temp image", ex);
            }
            finally
            {
                if (fs != null) fs.Close();
                _resetEvents[index].Set();
            }
        }
        #endregion 

        #region PrepareSnoopyMedia
        private void PrepareSnoopyMedia()
        { 
            string mediaLocation = ConfigurationManager.AppSettings["MediaLocation"].ToString();
            if (!Directory.Exists(mediaLocation))
                Directory.CreateDirectory(mediaLocation);
            SqlDataReader dr = SqlHelper.ExecuteReader(GetSQLSourceDataConnectionString("Snoopy"), CommandType.Text, "SELECT * FROM tblImage WHERE blobImage IS NOT NULL");
            int i = 0;
            _resetEvents = new ManualResetEvent[ThreadHelper.NUMBER_OF_THREADS];
            this.progressBar1.Maximum = ThreadHelper.NUMBER_OF_THREADS;
            while (dr.Read())
            {
                string fileType = dr["strImageType"].ToString();
                if (fileType == "image/pjpeg")
                    fileType = "jpeg";
                else if (fileType == "application/msword")
                    fileType = "doc";
                else if (fileType == "image/bmp")
                    fileType = "bmp";
                else fileType = "jpeg";

                string fileName = dr["lngImageID"].ToString() + "." + fileType;
                string fileFullName = mediaLocation + fileName;

                int index = i % ThreadHelper.NUMBER_OF_THREADS;
                object[] objs = new object[3];
                objs[0] = index;
                objs[1] = fileFullName;
                objs[2] = dr["blobImage"];
                _resetEvents[index] = new ManualResetEvent(false);
                ThreadPool.QueueUserWorkItem(new WaitCallback(StoreInTempLocation), objs); 

                if(index == ThreadHelper.NUMBER_OF_THREADS - 1)
                {
                    this.label_Message.Text = "Waiting...";
                    ThreadHelper.WaitAll(_resetEvents);
                    _resetEvents = new ManualResetEvent[ThreadHelper.NUMBER_OF_THREADS];
                }
                this.progressBar1.Value = index;
                this.label_Message.Text = (++i).ToString();
                Application.DoEvents();
            }
            this.progressBar1.Value = ThreadHelper.NUMBER_OF_THREADS;
            ThreadHelper.WaitAll(_resetEvents);
        }
        #endregion

        #endregion

        #endregion Media

        #region Build Stat Grid
        private void BuildInfoGrid()
        {
            _statTable = new DataTable();
            DataColumn column = null;
            column = new DataColumn();
            column.ColumnName = "Info";
            column.Caption = "Imported Data";
            column.DataType = System.Type.GetType("System.String");
            _statTable.Columns.Add(column);

            column = new DataColumn();
            column.ColumnName = "Count";
            column.Caption = "Number of Rows";
            column.DataType = System.Type.GetType("System.Int32");
            _statTable.Columns.Add(column);
            
            this.dataGridView1.DataSource = _statTable;
            
        }

        #endregion

        #region testConfigurationToolStripMenuItem_Click
        private void testConfigurationToolStripMenuItem_Click(object sender, EventArgs e)
        {   
            string sourceConnectionString  = string.Empty;
            try
            {
                sourceConnectionString = ConfigurationManager.ConnectionStrings["SourceDB"].ToString();
                 IDataReader dr = SqlHelper.ExecuteReader(sourceConnectionString, CommandType.Text, "select top 1 * from tblItems");
            }
            catch (Exception ex)
            {
                string message = string.Format("Connection String {0} is invalid.",sourceConnectionString);
                CCGeneral.LogAndDisplay.Error(message, ex);
                return;
            }

            string iXDataConnectionString = string.Empty;
            try
            {
                iXDataConnectionString = iView.iTrak.iTrakCommon.Database.ConnectionString;
                IDataReader dr = SqlHelper.ExecuteReader(iXDataConnectionString, CommandType.Text, "select top 1 * from AutoNumbers");
            }
            catch (Exception ex)
            {
                string message = string.Format("The iXData.xml might not copy to the running directory or the wrong one was copied.");
                CCGeneral.LogAndDisplay.Error(message, ex);
                return;
            }

            try
            {
                Property.ImportPropertyMappings(_appPath);
            }
            catch (Exception ex)
            {
                CCGeneral.LogAndDisplay.Error("The property name might not match the name in the iTrak Database", ex);
                return;
            }
            MessageBox.Show(this, "Test passed!");
        }
        #endregion


        #region Perspective
        private void btnImportData_Click(object sender, EventArgs e)
        {
            try
            {
                #region Prepare Data

                this.label_Message.Text = "Preparing data....Please wait....";
                Application.DoEvents();
                RunPackage(string.Empty, "TempObjectBegin.dtsx", DataSourceType.NONE);
                Property.ImportPropertyMappings(_appPath);
                RunPackage("Source", "Conversion_iBase.dtsx", DataSourceType.SQL);

                #endregion

                //#region Import Briefing Log

                //BriefingLog briefingLog = new BriefingLog();
                //this.progressBar1.Maximum = briefingLog.Count;
                //briefingLog.ImportedRowEvent += new System.EventHandler(this.ImportedDataEvent_Handler);
                //briefingLog.ImportCompletedEvent += new EventHandler(this.ImportCompletedEvent_Handler);
                //briefingLog.ImportBriefingLogs();
                //this.progressBar1.Value = 0;

             
                //#endregion

                //#region Import Blotter Log

                //Blotter blotter = new Blotter();
                //this.progressBar1.Maximum = blotter.Count;
                //blotter.ImportedRowEvent += new System.EventHandler(this.ImportedDataEvent_Handler);
                //blotter.ImportCompletedEvent += new EventHandler(this.ImportCompletedEvent_Handler);
                //blotter.ImportBlotters();
                //this.progressBar1.Value = 0;

                //#endregion

                //#region Import Incidents

                //DetailedReport incident = new DetailedReport();
                //this.progressBar1.Maximum = incident.Count;
                //incident.ImportedRowEvent += new System.EventHandler(this.ImportedDataEvent_Handler);
                //incident.ImportCompletedEvent += new EventHandler(this.ImportCompletedEvent_Handler);
                //incident.ImportIncidents2();
                //this.progressBar1.Value = 0;

                //#endregion

                //#region Import Subjects
                //Subject subject = new Subject();
                //this.progressBar1.Maximum = subject.Count;
                //subject.ImportedRowEvent += new System.EventHandler(this.ImportedDataEvent_Handler);
                //subject.ImportCompletedEvent += new EventHandler(this.ImportCompletedEvent_Handler);
                //subject.ImportSubjects2();
                //this.progressBar1.Value = 0;
                //#endregion

                //#region Import Employees
                //Employee employee = new Employee();
                //this.progressBar1.Maximum = employee.Count;
                //employee.ImportedRowEvent += new System.EventHandler(this.ImportedDataEvent_Handler);
                //employee.ImportCompletedEvent += new EventHandler(this.ImportCompletedEvent_Handler);
                //employee.ImportEmployee2();
                //this.progressBar1.Value = 0;
                //#endregion

                //#region Import Participant Assignment
                //ParticipantAssignment assignment = new ParticipantAssignment();
                //this.progressBar1.Maximum = assignment.Count;
                //assignment.ImportedRowEvent += new System.EventHandler(this.ImportedDataEvent_Handler);
                //assignment.ImportCompletedEvent += new EventHandler(this.ImportCompletedEvent_Handler);
                //assignment.ImportParticpantAssigments();
                //this.progressBar1.Value = 0;
                //#endregion

                //#region Import Game Audit

                //GameAudit gameAudit = new GameAudit();
                //this.progressBar1.Maximum = gameAudit.Count;
                //gameAudit.ImportedRowEvent += new System.EventHandler(this.ImportedDataEvent_Handler);
                //gameAudit.ImportCompletedEvent += new System.EventHandler(this.ImportCompletedEvent_Handler);
                //gameAudit.ImportGameAudits();

                //#endregion

                //#region Import Vehicles
                //Vehicle vehicle = new Vehicle();
                //this.progressBar1.Maximum = vehicle.Count;
                //vehicle.ImportedRowEvent += new System.EventHandler(this.ImportedDataEvent_Handler);
                //vehicle.ImportCompletedEvent += new System.EventHandler(this.ImportCompletedEvent_Handler);
                //vehicle.ImportVehicles();
                //#endregion

                //#region Import MultiNotes
                //MultiNotes multiNote = new MultiNotes();
                //this.progressBar1.Maximum = multiNote.Count;
                //multiNote.ImportedRowEvent += new System.EventHandler(this.ImportedDataEvent_Handler);
                //multiNote.ImportCompletedEvent += new System.EventHandler(this.ImportCompletedEvent_Handler);
                //multiNote.ImportMultiNotes();
                //#endregion

                //#region Import Saving & Losses

                //SavingsLossesForm savingLoss = new SavingsLossesForm();
                //this.progressBar1.Maximum = savingLoss.Count;
                //savingLoss.ImportedRowEvent += new System.EventHandler(this.ImportedDataEvent_Handler);
                //savingLoss.ImportCompletedEvent += new EventHandler(this.ImportCompletedEvent_Handler);
                //savingLoss.ImportSavingLosses();
                //this.progressBar1.Value = 0;

                //#endregion

                //#region Import Media
                //IncidentAttachment media = new IncidentAttachment();
                //this.progressBar1.Maximum = media.Count;
                //media.ImportedRowEvent += new System.EventHandler(this.ImportedDataEvent_Handler);
                //media.ImportCompletedEvent += new EventHandler(this.ImportCompletedEvent_Handler);
                //media.ImportMedia();
                //this.progressBar1.Value = 0;
                //#endregion

                RunPackage("Source", "PostUpdated.dtsx", DataSourceType.SQL);

#if !DEBUG
                RunPackage(string.Empty, "TempObjectEnd.dtsx", DataSourceType.NONE);
#endif
                this.listBox_Status.Items.Add("Import Data Successfully.");

                this.label_Message.Text = "Done.";
            }
            catch (Exception ex)
            {
                CCGeneral.LogAndDisplay.Error(ex.Message, ex);
            }
        }
        #endregion

        private void btnLoadData_Click(object sender, EventArgs e)
        {
            try
            {
                #region Prepare Data
                this.label_Message.Text = "Preparing data....Please wait....";
                Application.DoEvents();
                RunPackage(string.Empty, "TempObjectBegin.dtsx", DataSourceType.NONE);
                Property.ImportPropertyMappings(_appPath);
                RunPackage(string.Empty, "ConversionBegin.dtsx", DataSourceType.SQL);
                RunPackage("Seer", "Conversion.dtsx", DataSourceType.SQL);
                this.label_Message.Text = "Load data completed";
                #endregion
            }
            catch (Exception ex)
            {
                CCGeneral.LogAndDisplay.Error(ex.Message, ex);
            }
        }

        private void btnIncident_Click(object sender, EventArgs e)
        {
            try
            {
                #region Import Incidents
                DetailedReport incident = new DetailedReport();
                this.progressBar1.Maximum = incident.Count;
                incident.ImportedRowEvent += new System.EventHandler(this.ImportedDataEvent_Handler);
                incident.ImportCompletedEvent += new EventHandler(this.ImportCompletedEvent_Handler);
                incident.ImportIncidents2();
                this.progressBar1.Value = 0;
                this.label_Message.Text = "Convert Incident Completed";
                #endregion
            }
            catch (Exception ex)
            {
                CCGeneral.LogAndDisplay.Error(ex.Message, ex);
            }
        }

        private void btnConvertSubject_Click(object sender, EventArgs e)
        {
            try
            {
                #region Import Subjects
                Subject subject = new Subject();
                this.progressBar1.Maximum = subject.Count;
                subject.ImportedRowEvent += new System.EventHandler(this.ImportedDataEvent_Handler);
                subject.ImportCompletedEvent += new EventHandler(this.ImportCompletedEvent_Handler);
                //subject.ImportSubjects2();
                subject.ImportSubjects();
                this.progressBar1.Value = 0;
                this.label_Message.Text = "Convert Subject Completed";
                #endregion
            }
            catch (Exception ex)
            {
                CCGeneral.LogAndDisplay.Error(ex.Message, ex);
            }
        }

        private void btnConvertEmployee_Click(object sender, EventArgs e)
        {
            try
            {
                #region Import Employees
                Employee employee = new Employee();
                this.progressBar1.Maximum = employee.Count;
                employee.ImportedRowEvent += new System.EventHandler(this.ImportedDataEvent_Handler);
                employee.ImportCompletedEvent += new EventHandler(this.ImportCompletedEvent_Handler);
                employee.ImportEmployee2();
                this.progressBar1.Value = 0;
                this.label_Message.Text = "Convert Employee Completed";
                #endregion
            }
            catch (Exception ex)
            {
                CCGeneral.LogAndDisplay.Error(ex.Message, ex);
            }
        }

        private void btnFinalize_Click(object sender, EventArgs e)
        {
            try
            {
                #region Import Participant Assignment
                ParticipantAssignment assignment = new ParticipantAssignment();
                this.progressBar1.Maximum = assignment.Count;
                assignment.ImportedRowEvent += new System.EventHandler(this.ImportedDataEvent_Handler);
                assignment.ImportCompletedEvent += new EventHandler(this.ImportCompletedEvent_Handler);
                assignment.ImportParticpantAssigments();
                this.progressBar1.Value = 0;
                #endregion

                #region Import Media
                IncidentAttachment media = new IncidentAttachment();
                this.progressBar1.Maximum = media.Count;
                media.ImportedRowEvent += new System.EventHandler(this.ImportedDataEvent_Handler);
                media.ImportCompletedEvent += new EventHandler(this.ImportCompletedEvent_Handler);
                media.ImportMedia();
                this.progressBar1.Value = 0;
                #endregion

                //#region Import Vehicles
                //Vehicle vehicle = new Vehicle();
                //this.progressBar1.Maximum = vehicle.Count;
                //vehicle.ImportedRowEvent += new System.EventHandler(this.ImportedDataEvent_Handler);
                //vehicle.ImportCompletedEvent += new System.EventHandler(this.ImportCompletedEvent_Handler);
                //vehicle.ImportVehicles();
                //#endregion
                RunPackage("Seer", "PostUpdated.dtsx", DataSourceType.SQL);
#if !DEBUG
                RunPackage(string.Empty, "TempObjectEnd.dtsx", DataSourceType.NONE);
#endif
                this.listBox_Status.Items.Add("Import Data Successfully.");

                this.label_Message.Text = "Done.";
            }
            catch (Exception ex)
            {
                CCGeneral.LogAndDisplay.Error(ex.Message, ex);
            }
        }


    }
}