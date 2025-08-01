using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading.Channels;
using System.Linq;

namespace mainwin
{
    public partial class mainwin : Form
    {
        private string _connectionString;
        private string _backupFilePath;

        public mainwin()
        {
            InitializeComponent();
        }

        private async void connectionButton_Click(object sender, EventArgs e)
        {
            using (var form = new connectionwin())
            {
                if (form.ShowDialog() != DialogResult.OK)
                    return;

                _connectionString = form.ConnectionString;
                await LoadDatabasesAsync();
            }
        }

        private async void comboDataBaseBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            databaseGridView.DataSource = null;
            comboTableBox.Items.Clear();
            comboTableBox.Enabled = false;

            var db = comboDataBaseBox.SelectedItem as string;
            if (!String.IsNullOrEmpty(db))
                await LoadTablesAsync(db);
        }

        private async void comboTableBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            var db = comboDataBaseBox.SelectedItem as string;
            var tbl = comboTableBox.SelectedItem as string;
            if (!String.IsNullOrEmpty(db) && !String.IsNullOrEmpty(tbl))
            {
                await LoadTableDataAsync(db, tbl);
            }
            else
            {
                databaseGridView.DataSource = null;
            }
        }

        private async void refreshButton_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(_connectionString))
            {
                MessageBox.Show("Сначала подключитесь к серверу!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                await LoadDatabasesAsync();

                var db = comboDataBaseBox.SelectedItem as string;
                if (String.IsNullOrEmpty(db))
                {
                    databaseGridView.DataSource = null;
                    comboTableBox.Items.Clear();
                    comboTableBox.Enabled = false;
                    return;
                }

                await LoadTablesAsync(db);

                var tbl = comboTableBox.SelectedItem as string;
                if (!String.IsNullOrEmpty(tbl))
                {
                    await LoadTableDataAsync(db, tbl);
                }
                else
                {
                    databaseGridView.DataSource = null;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при обновлении сервера:\n{ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async Task LoadTableDataAsync(string database, string table)
        {
            databaseGridView.DataSource = null;

            var builder = new NpgsqlConnectionStringBuilder(_connectionString) { Database = database };
            using (var conn = new NpgsqlConnection(builder.ToString()))
            {
                await conn.OpenAsync();

                var sql = $@"SELECT * FROM public.""{table}"";";
                using (var cmd = new NpgsqlCommand(sql, conn) { CommandTimeout = 30 })
                {
                    try
                    {
                        using (var rdr = await cmd.ExecuteReaderAsync())
                        {
                            var dt = new DataTable();
                            dt.Load(rdr);
                            databaseGridView.DataSource = dt;
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Ошибка загрузки таблицы {table}:\n{ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private async Task LoadDataIntoGridView(string connStr)
        {
            try
            {
                using (var conn = new NpgsqlConnection(connStr))
                {
                    await conn.OpenAsync();
                    using (var cmd = new NpgsqlCommand("SELECT * FROM public.students", conn) { CommandTimeout = 30 })
                    using (var rdr = await cmd.ExecuteReaderAsync())
                    {
                        var dt = new DataTable();
                        dt.Load(rdr);
                        databaseGridView.DataSource = dt;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async Task LoadDatabasesAsync()
        {
            comboDataBaseBox.BeginUpdate();
            var previousDb = comboDataBaseBox.SelectedItem as string;
            comboDataBaseBox.Items.Clear();
            comboTableBox.Items.Clear();
            comboTableBox.Enabled = false;
            databaseGridView.DataSource = null;

            var csb = new NpgsqlConnectionStringBuilder(_connectionString) { Database = "postgres" };

            try
            {
                using (var conn = new NpgsqlConnection(csb.ToString()))
                {
                    await conn.OpenAsync();
                    const string sql = @"
                SELECT datname 
                FROM pg_database 
                WHERE datistemplate = false
                ORDER BY datname;";

                    using (var cmd = new NpgsqlCommand(sql, conn))
                    using (var rdr = await cmd.ExecuteReaderAsync())
                    {
                        var list = new List<string>();
                        while (await rdr.ReadAsync())
                            list.Add(rdr.GetString(0));
                        comboDataBaseBox.Items.AddRange(list.ToArray());

                        if (!string.IsNullOrEmpty(previousDb) && list.Contains(previousDb))
                            comboDataBaseBox.SelectedItem = previousDb;
                        else if (list.Count > 0)
                            comboDataBaseBox.SelectedIndex = 0;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при обновлении списка баз данных: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                comboDataBaseBox.EndUpdate();
            }
        }

        private async Task LoadTablesAsync(string database)
        {
            int maxRetries = 3;
            int delayBetweenRetries = 2000;

            for (int attempt = 1; attempt <= maxRetries; attempt++)
            {
                try
                {
                    // Явная очистка списка таблиц в UI-потоке
                    if (comboTableBox.InvokeRequired)
                    {
                        comboTableBox.Invoke(new Action(() =>
                        {
                            comboTableBox.Items.Clear();
                            comboTableBox.Text = "";
                        }));
                    }
                    else
                    {
                        comboTableBox.Items.Clear();
                        comboTableBox.Text = "";
                    }

                    var builder = new NpgsqlConnectionStringBuilder(_connectionString)
                    {
                        Database = database,
                        CommandTimeout = 60
                    };

                    using (var conn = new NpgsqlConnection(builder.ToString()))
                    {
                        await conn.OpenAsync();
                        const string sql = @"
                    SELECT tablename 
                    FROM pg_catalog.pg_tables 
                    WHERE schemaname = 'public'
                    ORDER BY tablename;";

                        using (var cmd = new NpgsqlCommand(sql, conn))
                        using (var rdr = await cmd.ExecuteReaderAsync())
                        {
                            var tables = new List<string>();
                            while (await rdr.ReadAsync())
                            {
                                tables.Add(rdr.GetString(0));
                            }

                            // Обновление списка таблиц в UI-потоке
                            if (comboTableBox.InvokeRequired)
                            {
                                comboTableBox.Invoke(new Action(() =>
                                {
                                    comboTableBox.BeginUpdate();
                                    comboTableBox.Items.Clear(); // Дополнительная очистка
                                    comboTableBox.Items.AddRange(tables.Distinct().ToArray()); // Устранение дубликатов
                                    comboTableBox.Enabled = tables.Count > 0;
                                    if (tables.Count > 0)
                                        comboTableBox.SelectedIndex = 0;
                                    comboTableBox.EndUpdate();
                                }));
                            }
                            else
                            {
                                comboTableBox.BeginUpdate();
                                comboTableBox.Items.Clear();
                                comboTableBox.Items.AddRange(tables.Distinct().ToArray());
                                comboTableBox.Enabled = tables.Count > 0;
                                if (tables.Count > 0)
                                    comboTableBox.SelectedIndex = 0;
                                comboTableBox.EndUpdate();
                            }
                        }
                    }
                    break;
                }
                catch (Exception ex)
                {
                    if (attempt == maxRetries)
                    {
                        MessageBox.Show($"Ошибка при загрузке таблиц:\n{ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        await Task.Delay(delayBetweenRetries);
                    }
                }
            }
        }

        private string FindPostgresUtility(string exeName)
        {
            var pathDirs = Environment.GetEnvironmentVariable("PATH")?.Split(Path.PathSeparator) ?? new string[0];
            foreach (var dir in pathDirs)
            {
                var full = Path.Combine(dir.Trim(), exeName);
                if (File.Exists(full)) return full;
            }
            foreach (var baseDir in new[] { @"C:\Program Files\PostgreSQL", @"C:\Program Files (x86)\PostgreSQL" })
            {
                if (!Directory.Exists(baseDir)) continue;
                foreach (var sub in Directory.GetDirectories(baseDir))
                {
                    var bin = Path.Combine(sub, "bin", exeName);
                    if (File.Exists(bin)) return bin;
                }
            }
            return null;
        }

        private void replicationButton_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(_connectionString))
            {
                MessageBox.Show("Сначала подключитесь к базе данных!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (comboDataBaseBox.SelectedItem == null)
            {
                MessageBox.Show("Выберите базу данных для резервного копирования!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string selectedDb = comboDataBaseBox.SelectedItem.ToString();

            using (var dlg = new SaveFileDialog
            {
                Filter = "Custom format (*.backup)|*.backup",
                Title = "Сохранить резервную копию"
            })
            {
                if (dlg.ShowDialog() != DialogResult.OK)
                    return;
                _backupFilePath = dlg.FileName;
            }

            string dumpExe = FindPostgresUtility("pg_dump.exe");
            if (dumpExe == null)
            {
                MessageBox.Show("Не найдена pg_dump.exe", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var csb = new Npgsql.NpgsqlConnectionStringBuilder(_connectionString)
            {
                Database = selectedDb // Обновляем имя базы данных по выбору пользователя
            };

            bool isSql = Path.GetExtension(_backupFilePath)
                             .Equals(".sql", StringComparison.OrdinalIgnoreCase);

            string args = isSql
                ? $"-h {csb.Host} -p {csb.Port} -U {csb.Username} -d {csb.Database} -f \"{_backupFilePath}\""
                : $"-h {csb.Host} -p {csb.Port} -U {csb.Username} -F c -b -v -f \"{_backupFilePath}\" {csb.Database}";

            var psi = new ProcessStartInfo(dumpExe, args)
            {
                UseShellExecute = false,
                CreateNoWindow = true,
                RedirectStandardOutput = true,
                RedirectStandardError = true
            };
            psi.EnvironmentVariables["PGPASSWORD"] = csb.Password;

            try
            {
                using (var proc = Process.Start(psi))
                {
                    proc.BeginOutputReadLine();
                    proc.BeginErrorReadLine();
                    proc.WaitForExit();

                    if (proc.ExitCode == 0)
                        MessageBox.Show($"Резервная копия БД \"{csb.Database}\" успешно создана.", "Готово", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    else
                        MessageBox.Show("Ошибка при создании резервной копии.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void playbackButton_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(_connectionString))
            {
                MessageBox.Show("Сначала подключитесь к базе данных!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Выбор файла резервной копии
            string backupFilePath;
            using (var open = new OpenFileDialog
            {
                Filter = "Файлы резервных копий баз данных (*.backup)|*.backup|Все файлы (*.*)|*.*",
                Title = "Выберите файл резервной копии"
            })
            {
                if (open.ShowDialog() != DialogResult.OK)
                    return;
                backupFilePath = open.FileName;
            }

            // Поиск pg_restore.exe
            var pgRestorePath = FindPostgresUtility("pg_restore.exe");
            if (pgRestorePath == null)
            {
                MessageBox.Show("Не удалось найти pg_restore.exe", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                var csb = new NpgsqlConnectionStringBuilder(_connectionString);

                // Получаем выбранную базу данных из comboDataBaseBox
                if (comboDataBaseBox.SelectedItem == null)
                {
                    MessageBox.Show("Выберите базу данных из списка.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                string dbName = comboDataBaseBox.SelectedItem.ToString();
                csb.Database = dbName;

                // Подключение к postgres для удаления и создания базы
                var adminCsb = new NpgsqlConnectionStringBuilder(_connectionString)
                {
                    Database = "postgres"
                };

                using (var adminConn = new NpgsqlConnection(adminCsb.ToString()))
                {
                    await adminConn.OpenAsync();

                    // Проверка существования базы
                    bool exists;
                    using (var cmd = new NpgsqlCommand("SELECT 1 FROM pg_database WHERE datname = @db", adminConn))
                    {
                        cmd.Parameters.AddWithValue("db", dbName);
                        exists = await cmd.ExecuteScalarAsync() != null;
                    }

                    if (exists)
                    {
                        var dr = MessageBox.Show(
                            "Внимание! После выполнения данного процесса добавленные новые данные будут удалены. Перед выполнением воспроизведения рекомендуется сделать резервное копирование!\nПродолжить?",
                            "Подтвердите воспроизведение",
                            MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                        if (dr != DialogResult.Yes)
                            return;

                        // Завершение активных соединений
                        using (var term = new NpgsqlCommand(@"
                    SELECT pg_terminate_backend(pid)
                      FROM pg_stat_activity
                     WHERE datname = @db AND pid <> pg_backend_pid();", adminConn))
                        {
                            term.Parameters.AddWithValue("db", dbName);
                            await term.ExecuteNonQueryAsync();
                        }

                        // Удаление базы
                        using (var drop = new NpgsqlCommand($@"DROP DATABASE ""{dbName}"";", adminConn))
                        {
                            await drop.ExecuteNonQueryAsync();
                        }
                    }

                    // Создание новой базы
                    using (var create = new NpgsqlCommand($@"CREATE DATABASE ""{dbName}"";", adminConn))
                    {
                        await create.ExecuteNonQueryAsync();
                    }
                }

                // Восстановление через pg_restore
                var restoreCsb = new NpgsqlConnectionStringBuilder(_connectionString)
                {
                    Pooling = false,
                    Database = dbName
                };

                var psi = new ProcessStartInfo
                {
                    FileName = pgRestorePath,
                    Arguments = $"-h {restoreCsb.Host} -p {restoreCsb.Port} -U {restoreCsb.Username} -d {dbName} -v \"{backupFilePath}\"",
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    CreateNoWindow = true
                };
                psi.EnvironmentVariables["PGPASSWORD"] = restoreCsb.Password;

                using (var proc = Process.Start(psi))
                {
                    string stdout = proc.StandardOutput.ReadToEnd();
                    string stderr = proc.StandardError.ReadToEnd();
                    proc.WaitForExit();

                    if (proc.ExitCode != 0)
                        throw new Exception($"pg_restore завершился с кодом {proc.ExitCode}:\n{stderr}");
                }

                // Очистка пула соединений
                NpgsqlConnection.ClearAllPools();

                // Обновление DataGridView
                var refreshedCsb = new NpgsqlConnectionStringBuilder(_connectionString)
                {
                    Database = dbName
                };
                await LoadDataIntoGridView(refreshedCsb.ToString());

                MessageBox.Show("Воспроизведение выполнено успешно.", "Готово", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при воспроизведение БД:\n{ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}