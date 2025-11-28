using System;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Windows.Forms;

namespace BudgetApp
{
    public class DatabaseHelper
    {
        private string connectionString;
        private string dbPath;

        public DatabaseHelper()
        {
            // Используем текущую директорию вместо сетевого пути
            dbPath = Path.Combine(Directory.GetCurrentDirectory(), "BudgetDB.accdb");
            connectionString = $"Provider=Microsoft.ACE .OLEDB.16.0;Data Source={dbPath};";

            CheckDatabase();
        }

        private void CheckDatabase()
        {
            if (!File.Exists(dbPath))
            {
                MessageBox.Show($"База данных не найдена!\nСоздайте пустой файл Access: {dbPath}\nИли используйте тестовый режим.", "Внимание");
                // Можно создать простую текстовую базу как временное решение
                CreateSimpleDatabase();
            }
        }

        private void CreateSimpleDatabase()
        {
            // Временное решение - создаем текстовый файл для хранения данных
            string txtDbPath = Path.Combine(Directory.GetCurrentDirectory(), "BudgetData.txt");
            if (!File.Exists(txtDbPath))
            {
                File.WriteAllText(txtDbPath, "ID,Date,Amount,Category,Description,Type\r\n");
            }
        }

        public DataTable GetTransactions()
        {
            DataTable dt = new DataTable();

            // Если Access база существует, используем ее
            if (File.Exists(dbPath))
            {
                try
                {
                    using (OleDbConnection conn = new OleDbConnection(connectionString))
                    {
                        conn.Open();
                        string query = "SELECT * FROM Transactions ORDER BY Date DESC";
                        using (OleDbCommand cmd = new OleDbCommand(query, conn))
                        {
                            using (OleDbDataAdapter adapter = new OleDbDataAdapter(cmd))
                            {
                                adapter.Fill(dt);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка загрузки из Access: {ex.Message}");
                }
            }
            else
            {
                // Иначе используем тестовые данные
                CreateTestData(dt);
            }

            return dt;
        }

        private void CreateTestData(DataTable dt)
        {
            // Создаем структуру таблицы
            dt.Columns.Add("ID", typeof(int));
            dt.Columns.Add("Date", typeof(DateTime));
            dt.Columns.Add("Amount", typeof(decimal));
            dt.Columns.Add("Category", typeof(string));
            dt.Columns.Add("Description", typeof(string));
            dt.Columns.Add("Type", typeof(string));

            // Добавляем тестовые данные
            dt.Rows.Add(1, DateTime.Now.AddDays(-2), 50000, "Зарплата", "Зарплата за месяц", "Доход");
            dt.Rows.Add(2, DateTime.Now.AddDays(-1), 1500, "Продукты", "Покупка продуктов", "Расход");
            dt.Rows.Add(3, DateTime.Now, 500, "Транспорт", "Проездной", "Расход");
        }

        public bool AddTransaction(DateTime date, decimal amount, string category, string description, string type)
        {
            if (File.Exists(dbPath))
            {
                try
                {
                    using (OleDbConnection conn = new OleDbConnection(connectionString))
                    {
                        conn.Open();
                        string query = @"INSERT INTO Transactions (Date, Amount, Category, Description, Type) 
                                       VALUES (@Date, @Amount, @Category, @Description, @Type)";

                        using (OleDbCommand cmd = new OleDbCommand(query, conn))
                        {
                            cmd.Parameters.AddWithValue("@Date", date);
                            cmd.Parameters.AddWithValue("@Amount", amount);
                            cmd.Parameters.AddWithValue("@Category", category);
                            cmd.Parameters.AddWithValue("@Description", description);
                            cmd.Parameters.AddWithValue("@Type", type);

                            return cmd.ExecuteNonQuery() > 0;
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка добавления: {ex.Message}");
                    return false;
                }
            }
            else
            {
                MessageBox.Show("Добавление в тестовом режиме. Данные не сохранятся.");
                return true; // Возвращаем true для продолжения работы
            }
        }

        public bool DeleteTransaction(int id)
        {
            if (File.Exists(dbPath))
            {
                try
                {
                    using (OleDbConnection conn = new OleDbConnection(connectionString))
                    {
                        conn.Open();
                        string query = "DELETE FROM Transactions WHERE ID = @ID";

                        using (OleDbCommand cmd = new OleDbCommand(query, conn))
                        {
                            cmd.Parameters.AddWithValue("@ID", id);
                            return cmd.ExecuteNonQuery() > 0;
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка удаления: {ex.Message}");
                    return false;
                }
            }
            else
            {
                MessageBox.Show("Удаление в тестовом режиме.");
                return true;
            }
        }

        public decimal GetBalance()
        {
            decimal balance = 0;

            if (File.Exists(dbPath))
            {
                try
                {
                    using (OleDbConnection conn = new OleDbConnection(connectionString))
                    {
                        conn.Open();
                        string query = "SELECT SUM(IIF(Type='Доход', Amount, -Amount)) FROM Transactions";

                        using (OleDbCommand cmd = new OleDbCommand(query, conn))
                        {
                            var result = cmd.ExecuteScalar();
                            if (result != DBNull.Value && result != null)
                            {
                                balance = Convert.ToDecimal(result);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    // Игнорируем ошибки
                }
            }
            else
            {
                // Тестовый баланс
                balance = 50000 - 1500 - 500;
            }

            return balance;
        }
    }
}