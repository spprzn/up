using System;
using System.Drawing;
using System.Windows.Forms;

namespace FinancialCalculator
{
    public partial class CalculatorForm : Form
    {
        public CalculatorForm()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.Text = "Финансовый калькулятор";
            this.Size = new Size(1000, 700);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.AutoScroll = true;

            CreateControls();
        }

        private void CreateControls()
        {
            TabControl tabControl = new TabControl
            {
                Dock = DockStyle.Fill,
                Padding = new Point(10, 10)
            };

            // Вкладка 1: Сложные проценты
            TabPage compoundTab = new TabPage("Сложные проценты");
            compoundTab.AutoScroll = true;
            CreateCompoundInterestTab(compoundTab);

            // Вкладка 2: Кредитный калькулятор
            TabPage loanTab = new TabPage("Кредитный калькулятор");
            loanTab.AutoScroll = true;
            CreateLoanCalculatorTab(loanTab);

            // Вкладка 3: Финансовые цели
            TabPage goalsTab = new TabPage("Финансовые цели");
            goalsTab.AutoScroll = true;
            CreateGoalsTab(goalsTab);

            // Вкладка 4: Инвестиции
            TabPage investmentTab = new TabPage("Инвестиции");
            investmentTab.AutoScroll = true;
            CreateInvestmentTab(investmentTab);

            // Вкладка 5: Пенсионный калькулятор
            TabPage retirementTab = new TabPage("Пенсионный калькулятор");
            retirementTab.AutoScroll = true;
            CreateRetirementTab(retirementTab);

            tabControl.TabPages.AddRange(new TabPage[] { compoundTab, loanTab, goalsTab, investmentTab, retirementTab });
            this.Controls.Add(tabControl);
        }

        private void CreateCompoundInterestTab(TabPage tab)
        {
            tab.Padding = new Padding(10);

            // Группа ввода параметров
            GroupBox inputGroup = new GroupBox
            {
                Text = "Параметры расчета сложных процентов",
                Location = new Point(10, 10),
                Size = new Size(950, 120),
                Font = new Font("Arial", 9, FontStyle.Bold)
            };

            // Первая строка ввода
            Label lblPrincipal = new Label { Text = "Начальная сумма (₽):", Location = new Point(15, 25), Width = 140, Font = new Font("Arial", 9) };
            TextBox txtPrincipal = new TextBox { Location = new Point(160, 22), Width = 120, Text = "100000", Font = new Font("Arial", 9) };

            Label lblMonthly = new Label { Text = "Ежемесячное пополнение (₽):", Location = new Point(300, 25), Width = 160, Font = new Font("Arial", 9) };
            TextBox txtMonthly = new TextBox { Location = new Point(465, 22), Width = 120, Text = "10000", Font = new Font("Arial", 9) };

            // Вторая строка ввода
            Label lblRate = new Label { Text = "Годовая ставка (%):", Location = new Point(15, 55), Width = 120, Font = new Font("Arial", 9) };
            TextBox txtRate = new TextBox { Location = new Point(160, 52), Width = 80, Text = "12", Font = new Font("Arial", 9) };

            Label lblYears = new Label { Text = "Срок (лет):", Location = new Point(260, 55), Width = 80, Font = new Font("Arial", 9) };
            TextBox txtYears = new TextBox { Location = new Point(345, 52), Width = 60, Text = "10", Font = new Font("Arial", 9) };

            // Кнопка расчета
            Button btnCalculate = new Button
            {
                Text = "Рассчитать",
                Location = new Point(450, 50),
                Size = new Size(120, 30),
                BackColor = Color.LightGreen,
                Font = new Font("Arial", 9, FontStyle.Bold)
            };

            inputGroup.Controls.AddRange(new Control[] {
                lblPrincipal, txtPrincipal, lblMonthly, txtMonthly,
                lblRate, txtRate, lblYears, txtYears, btnCalculate
            });

            // Группа результатов
            GroupBox resultGroup = new GroupBox
            {
                Text = "Результаты расчета",
                Location = new Point(10, 140),
                Size = new Size(950, 180),
                Font = new Font("Arial", 9, FontStyle.Bold)
            };

            // Левый столбец результатов
            Label lblTotal = new Label { Text = "Итоговая сумма: 0 ₽", Location = new Point(15, 30), Width = 400, Font = new Font("Arial", 10, FontStyle.Bold), ForeColor = Color.DarkBlue };
            Label lblInvested = new Label { Text = "Всего вложено: 0 ₽", Location = new Point(15, 60), Width = 400, Font = new Font("Arial", 9) };
            Label lblProfit = new Label { Text = "Доход от процентов: 0 ₽", Location = new Point(15, 90), Width = 400, Font = new Font("Arial", 9) };
            Label lblAnnualProfit = new Label { Text = "Среднегодовой доход: 0 ₽", Location = new Point(15, 120), Width = 400, Font = new Font("Arial", 9) };

            // Правый столбец результатов
            Label lblMonthlyResult = new Label { Text = "Ежемесячный рост: 0 ₽", Location = new Point(450, 30), Width = 400, Font = new Font("Arial", 9) };
            Label lblYearsToDouble = new Label { Text = "Удвоение капитала за: 0 лет", Location = new Point(450, 60), Width = 400, Font = new Font("Arial", 9) };
            Label lblTotalMonths = new Label { Text = "Всего месяцев: 0", Location = new Point(450, 90), Width = 400, Font = new Font("Arial", 9) };
            Label lblMonthlyRate = new Label { Text = "Месячная ставка: 0%", Location = new Point(450, 120), Width = 400, Font = new Font("Arial", 9) };

            resultGroup.Controls.AddRange(new Control[] {
                lblTotal, lblInvested, lblProfit, lblAnnualProfit,
                lblMonthlyResult, lblYearsToDouble, lblTotalMonths, lblMonthlyRate
            });

            // Информационная группа
            GroupBox infoGroup = new GroupBox
            {
                Text = "Как это работает?",
                Location = new Point(10, 330),
                Size = new Size(950, 80),
                Font = new Font("Arial", 9, FontStyle.Bold)
            };

            Label lblInfo = new Label
            {
                Text = "Сложные проценты - это начисление процентов на первоначальную сумму и на уже накопленные проценты. " +
                       "При регулярных пополнениях капитал растет в геометрической прогрессии.",
                Location = new Point(15, 25),
                Size = new Size(920, 40),
                Font = new Font("Arial", 8),
                ForeColor = Color.DarkSlateGray
            };

            infoGroup.Controls.Add(lblInfo);

            tab.Controls.AddRange(new Control[] { inputGroup, resultGroup, infoGroup });

            // Обработчик расчета
            btnCalculate.Click += (s, e) => CalculateCompoundInterest(txtPrincipal, txtMonthly, txtRate, txtYears,
                lblTotal, lblInvested, lblProfit, lblAnnualProfit, lblMonthlyResult, lblYearsToDouble, lblTotalMonths, lblMonthlyRate);
        }

        private void CreateLoanCalculatorTab(TabPage tab)
        {
            tab.Padding = new Padding(10);

            GroupBox inputGroup = new GroupBox
            {
                Text = "Параметры кредита",
                Location = new Point(10, 10),
                Size = new Size(950, 120),
                Font = new Font("Arial", 9, FontStyle.Bold)
            };

            // Первая строка
            Label lblAmount = new Label { Text = "Сумма кредита (₽):", Location = new Point(15, 25), Width = 120, Font = new Font("Arial", 9) };
            TextBox txtAmount = new TextBox { Location = new Point(140, 22), Width = 120, Text = "1000000", Font = new Font("Arial", 9) };

            Label lblRate = new Label { Text = "Годовая ставка (%):", Location = new Point(280, 25), Width = 120, Font = new Font("Arial", 9) };
            TextBox txtRate = new TextBox { Location = new Point(405, 22), Width = 80, Text = "12", Font = new Font("Arial", 9) };

            Label lblTerm = new Label { Text = "Срок (лет):", Location = new Point(505, 25), Width = 80, Font = new Font("Arial", 9) };
            TextBox txtTerm = new TextBox { Location = new Point(590, 22), Width = 60, Text = "5", Font = new Font("Arial", 9) };

            // Вторая строка
            Label lblPaymentType = new Label { Text = "Тип платежа:", Location = new Point(15, 60), Width = 80, Font = new Font("Arial", 9) };
            ComboBox cmbPaymentType = new ComboBox
            {
                Location = new Point(100, 57),
                Size = new Size(150, 25),
                DropDownStyle = ComboBoxStyle.DropDownList,
                Font = new Font("Arial", 9)
            };
            cmbPaymentType.Items.AddRange(new string[] { "Аннуитетный", "Дифференцированный" });
            cmbPaymentType.SelectedIndex = 0;

            Button btnCalculate = new Button
            {
                Text = "Рассчитать кредит",
                Location = new Point(280, 55),
                Size = new Size(150, 30),
                BackColor = Color.LightBlue,
                Font = new Font("Arial", 9, FontStyle.Bold)
            };

            inputGroup.Controls.AddRange(new Control[] {
                lblAmount, txtAmount, lblRate, txtRate, lblTerm, txtTerm,
                lblPaymentType, cmbPaymentType, btnCalculate
            });

            // Результаты
            GroupBox resultGroup = new GroupBox
            {
                Text = "Результаты расчета кредита",
                Location = new Point(10, 140),
                Size = new Size(950, 150),
                Font = new Font("Arial", 9, FontStyle.Bold)
            };

            // Левый столбец
            Label lblMonthlyPayment = new Label { Text = "Ежемесячный платеж: 0 ₽", Location = new Point(15, 30), Width = 400, Font = new Font("Arial", 10, FontStyle.Bold), ForeColor = Color.DarkBlue };
            Label lblTotalPayment = new Label { Text = "Общая сумма выплат: 0 ₽", Location = new Point(15, 60), Width = 400, Font = new Font("Arial", 9) };
            Label lblOverpayment = new Label { Text = "Переплата по кредиту: 0 ₽", Location = new Point(15, 90), Width = 400, Font = new Font("Arial", 9) };

            // Правый столбец
            Label lblEffectiveRate = new Label { Text = "Эффективная ставка: 0%", Location = new Point(450, 30), Width = 400, Font = new Font("Arial", 9) };
            Label lblFirstPayment = new Label { Text = "Первый платеж: 0 ₽", Location = new Point(450, 60), Width = 400, Font = new Font("Arial", 9) };
            Label lblLastPayment = new Label { Text = "Последний платеж: 0 ₽", Location = new Point(450, 90), Width = 400, Font = new Font("Arial", 9) };

            resultGroup.Controls.AddRange(new Control[] {
                lblMonthlyPayment, lblTotalPayment, lblOverpayment,
                lblEffectiveRate, lblFirstPayment, lblLastPayment
            });

            tab.Controls.AddRange(new Control[] { inputGroup, resultGroup });

            btnCalculate.Click += (s, e) => CalculateLoan(txtAmount, txtRate, txtTerm, cmbPaymentType,
                lblMonthlyPayment, lblTotalPayment, lblOverpayment, lblEffectiveRate, lblFirstPayment, lblLastPayment);
        }

        private void CreateGoalsTab(TabPage tab)
        {
            tab.Padding = new Padding(10);

            GroupBox goalGroup = new GroupBox
            {
                Text = "Параметры финансовой цели",
                Location = new Point(10, 10),
                Size = new Size(950, 100),
                Font = new Font("Arial", 9, FontStyle.Bold)
            };

            Label lblGoalAmount = new Label { Text = "Сумма цели (₽):", Location = new Point(15, 30), Width = 110, Font = new Font("Arial", 9) };
            TextBox txtGoalAmount = new TextBox { Location = new Point(130, 27), Width = 120, Text = "1000000", Font = new Font("Arial", 9) };

            Label lblGoalYears = new Label { Text = "Срок (лет):", Location = new Point(270, 30), Width = 80, Font = new Font("Arial", 9) };
            TextBox txtGoalYears = new TextBox { Location = new Point(355, 27), Width = 60, Text = "5", Font = new Font("Arial", 9) };

            Label lblGoalRate = new Label { Text = "Доходность (% год):", Location = new Point(435, 30), Width = 120, Font = new Font("Arial", 9) };
            TextBox txtGoalRate = new TextBox { Location = new Point(560, 27), Width = 60, Text = "10", Font = new Font("Arial", 9) };

            Button btnCalculateGoal = new Button
            {
                Text = "Рассчитать накопления",
                Location = new Point(650, 25),
                Size = new Size(150, 30),
                BackColor = Color.LightYellow,
                Font = new Font("Arial", 9, FontStyle.Bold)
            };

            goalGroup.Controls.AddRange(new Control[] {
                lblGoalAmount, txtGoalAmount, lblGoalYears, txtGoalYears,
                lblGoalRate, txtGoalRate, btnCalculateGoal
            });

            GroupBox resultGroup = new GroupBox
            {
                Text = "Результаты планирования цели",
                Location = new Point(10, 120),
                Size = new Size(950, 130),
                Font = new Font("Arial", 9, FontStyle.Bold)
            };

            Label lblMonthlySave = new Label { Text = "Ежемесячное пополнение: 0 ₽", Location = new Point(15, 30), Width = 400, Font = new Font("Arial", 10, FontStyle.Bold), ForeColor = Color.DarkBlue };
            Label lblTotalSave = new Label { Text = "Всего нужно накопить: 0 ₽", Location = new Point(15, 60), Width = 400, Font = new Font("Arial", 9) };
            Label lblGrowth = new Label { Text = "Рост от процентов: 0 ₽", Location = new Point(15, 90), Width = 400, Font = new Font("Arial", 9) };

            Label lblMonthlyWithout = new Label { Text = "Без инвестиций в месяц: 0 ₽", Location = new Point(450, 30), Width = 400, Font = new Font("Arial", 9) };
            Label lblEconomy = new Label { Text = "Экономия за счет %: 0 ₽", Location = new Point(450, 60), Width = 400, Font = new Font("Arial", 9) };

            resultGroup.Controls.AddRange(new Control[] {
                lblMonthlySave, lblTotalSave, lblGrowth,
                lblMonthlyWithout, lblEconomy
            });

            tab.Controls.AddRange(new Control[] { goalGroup, resultGroup });

            btnCalculateGoal.Click += (s, e) => CalculateGoal(txtGoalAmount, txtGoalYears, txtGoalRate,
                lblMonthlySave, lblTotalSave, lblGrowth, lblMonthlyWithout, lblEconomy);
        }

        private void CreateInvestmentTab(TabPage tab)
        {
            tab.Padding = new Padding(10);

            GroupBox investmentGroup = new GroupBox
            {
                Text = "Расчет доходности инвестиций",
                Location = new Point(10, 10),
                Size = new Size(950, 100),
                Font = new Font("Arial", 9, FontStyle.Bold)
            };

            Label lblInvAmount = new Label { Text = "Сумма инвестиций (₽):", Location = new Point(15, 30), Width = 140, Font = new Font("Arial", 9) };
            TextBox txtInvAmount = new TextBox { Location = new Point(160, 27), Width = 120, Text = "100000", Font = new Font("Arial", 9) };

            Label lblInvRate = new Label { Text = "Ожидаемая доходность (% год):", Location = new Point(300, 30), Width = 170, Font = new Font("Arial", 9) };
            TextBox txtInvRate = new TextBox { Location = new Point(475, 27), Width = 60, Text = "15", Font = new Font("Arial", 9) };

            Label lblInvYears = new Label { Text = "Срок (лет):", Location = new Point(555, 30), Width = 80, Font = new Font("Arial", 9) };
            TextBox txtInvYears = new TextBox { Location = new Point(640, 27), Width = 60, Text = "10", Font = new Font("Arial", 9) };

            Button btnCalculateInv = new Button
            {
                Text = "Рассчитать доходность",
                Location = new Point(720, 25),
                Size = new Size(150, 30),
                BackColor = Color.LightCyan,
                Font = new Font("Arial", 9, FontStyle.Bold)
            };

            investmentGroup.Controls.AddRange(new Control[] {
                lblInvAmount, txtInvAmount, lblInvRate, txtInvRate,
                lblInvYears, txtInvYears, btnCalculateInv
            });

            GroupBox invResultGroup = new GroupBox
            {
                Text = "Результаты инвестиций",
                Location = new Point(10, 120),
                Size = new Size(950, 150),
                Font = new Font("Arial", 9, FontStyle.Bold)
            };

            Label lblFutureValue = new Label { Text = "Будущая стоимость: 0 ₽", Location = new Point(15, 30), Width = 400, Font = new Font("Arial", 10, FontStyle.Bold), ForeColor = Color.DarkBlue };
            Label lblTotalReturn = new Label { Text = "Общая доходность: 0 ₽", Location = new Point(15, 60), Width = 400, Font = new Font("Arial", 9) };
            Label lblAnnualReturn = new Label { Text = "Среднегодовая доходность: 0%", Location = new Point(15, 90), Width = 400, Font = new Font("Arial", 9) };
            Label lblDoubleTime = new Label { Text = "Удвоение капитала за: 0 лет", Location = new Point(15, 120), Width = 400, Font = new Font("Arial", 9) };

            Label lblMonthlyGrowth = new Label { Text = "Среднемесячный рост: 0 ₽", Location = new Point(450, 30), Width = 400, Font = new Font("Arial", 9) };
            Label lblROI = new Label { Text = "ROI (возврат инвестиций): 0%", Location = new Point(450, 60), Width = 400, Font = new Font("Arial", 9) };

            invResultGroup.Controls.AddRange(new Control[] {
                lblFutureValue, lblTotalReturn, lblAnnualReturn, lblDoubleTime,
                lblMonthlyGrowth, lblROI
            });

            tab.Controls.AddRange(new Control[] { investmentGroup, invResultGroup });

            btnCalculateInv.Click += (s, e) => CalculateInvestment(txtInvAmount, txtInvRate, txtInvYears,
                lblFutureValue, lblTotalReturn, lblAnnualReturn, lblDoubleTime, lblMonthlyGrowth, lblROI);
        }

        private void CreateRetirementTab(TabPage tab)
        {
            tab.Padding = new Padding(10);

            GroupBox retirementGroup = new GroupBox
            {
                Text = "Параметры пенсионных накоплений",
                Location = new Point(10, 10),
                Size = new Size(950, 120),
                Font = new Font("Arial", 9, FontStyle.Bold)
            };

            Label lblCurrentAge = new Label { Text = "Текущий возраст (лет):", Location = new Point(15, 30), Width = 140, Font = new Font("Arial", 9) };
            TextBox txtCurrentAge = new TextBox { Location = new Point(160, 27), Width = 60, Text = "30", Font = new Font("Arial", 9) };

            Label lblRetirementAge = new Label { Text = "Пенсионный возраст (лет):", Location = new Point(240, 30), Width = 150, Font = new Font("Arial", 9) };
            TextBox txtRetirementAge = new TextBox { Location = new Point(395, 27), Width = 60, Text = "60", Font = new Font("Arial", 9) };

            Label lblMonthlyPension = new Label { Text = "Желаемая пенсия (₽/мес):", Location = new Point(475, 30), Width = 150, Font = new Font("Arial", 9) };
            TextBox txtMonthlyPension = new TextBox { Location = new Point(630, 27), Width = 100, Text = "50000", Font = new Font("Arial", 9) };

            Label lblPensionRate = new Label { Text = "Доходность (% год):", Location = new Point(15, 70), Width = 120, Font = new Font("Arial", 9) };
            TextBox txtPensionRate = new TextBox { Location = new Point(140, 67), Width = 60, Text = "8", Font = new Font("Arial", 9) };

            Button btnCalculatePension = new Button
            {
                Text = "Рассчитать накопления",
                Location = new Point(220, 65),
                Size = new Size(150, 30),
                BackColor = Color.LightSalmon,
                Font = new Font("Arial", 9, FontStyle.Bold)
            };

            retirementGroup.Controls.AddRange(new Control[] {
                lblCurrentAge, txtCurrentAge, lblRetirementAge, txtRetirementAge,
                lblMonthlyPension, txtMonthlyPension, lblPensionRate, txtPensionRate,
                btnCalculatePension
            });

            GroupBox pensionResultGroup = new GroupBox
            {
                Text = "Результаты пенсионного планирования",
                Location = new Point(10, 140),
                Size = new Size(950, 150),
                Font = new Font("Arial", 9, FontStyle.Bold)
            };

            Label lblPensionCapital = new Label { Text = "Необходимый капитал: 0 ₽", Location = new Point(15, 30), Width = 400, Font = new Font("Arial", 10, FontStyle.Bold), ForeColor = Color.DarkBlue };
            Label lblMonthlySavePension = new Label { Text = "Ежемесячное пополнение: 0 ₽", Location = new Point(15, 60), Width = 400, Font = new Font("Arial", 9) };
            Label lblTotalSavePension = new Label { Text = "Всего накоплено к пенсии: 0 ₽", Location = new Point(15, 90), Width = 400, Font = new Font("Arial", 9) };
            Label lblPensionDuration = new Label { Text = "Пенсионные накопления на: 0 лет", Location = new Point(15, 120), Width = 400, Font = new Font("Arial", 9) };

            Label lblWorkingYears = new Label { Text = "Лет до пенсии: 0", Location = new Point(450, 30), Width = 400, Font = new Font("Arial", 9) };
            Label lblAnnualPension = new Label { Text = "Годовая пенсия: 0 ₽", Location = new Point(450, 60), Width = 400, Font = new Font("Arial", 9) };

            pensionResultGroup.Controls.AddRange(new Control[] {
                lblPensionCapital, lblMonthlySavePension, lblTotalSavePension, lblPensionDuration,
                lblWorkingYears, lblAnnualPension
            });

            tab.Controls.AddRange(new Control[] { retirementGroup, pensionResultGroup });

            btnCalculatePension.Click += (s, e) => CalculatePension(txtCurrentAge, txtRetirementAge, txtMonthlyPension, txtPensionRate,
                lblPensionCapital, lblMonthlySavePension, lblTotalSavePension, lblPensionDuration, lblWorkingYears, lblAnnualPension);
        }

        // Методы расчетов (остаются без изменений)
        private void CalculateCompoundInterest(TextBox txtPrincipal, TextBox txtMonthly, TextBox txtRate, TextBox txtYears,
                                             Label lblTotal, Label lblInvested, Label lblProfit, Label lblAnnualProfit,
                                             Label lblMonthlyResult, Label lblYearsToDouble, Label lblTotalMonths, Label lblMonthlyRate)
        {
            try
            {
                double principal = double.Parse(txtPrincipal.Text);
                double monthly = double.Parse(txtMonthly.Text);
                double rate = double.Parse(txtRate.Text) / 100;
                int years = int.Parse(txtYears.Text);

                double monthlyRate = rate / 12;
                int months = years * 12;

                double total = principal * Math.Pow(1 + monthlyRate, months);
                total += monthly * (Math.Pow(1 + monthlyRate, months) - 1) / monthlyRate;

                double totalInvested = principal + monthly * months;
                double profit = total - totalInvested;
                double annualProfit = profit / years;
                double monthlyGrowth = profit / months;
                double yearsToDouble = Math.Log(2) / Math.Log(1 + rate);

                lblTotal.Text = $"Итоговая сумма: {total:C0}";
                lblInvested.Text = $"Всего вложено: {totalInvested:C0}";
                lblProfit.Text = $"Доход от процентов: {profit:C0}";
                lblAnnualProfit.Text = $"Среднегодовой доход: {annualProfit:C0}";
                lblMonthlyResult.Text = $"Ежемесячный рост: {monthlyGrowth:C0}";
                lblYearsToDouble.Text = $"Удвоение капитала за: {yearsToDouble:F1} лет";
                lblTotalMonths.Text = $"Всего месяцев: {months}";
                lblMonthlyRate.Text = $"Месячная ставка: {monthlyRate * 100:F2}%";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка расчета: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CalculateLoan(TextBox txtAmount, TextBox txtRate, TextBox txtTerm, ComboBox cmbPaymentType,
                                 Label lblMonthlyPayment, Label lblTotalPayment, Label lblOverpayment, Label lblEffectiveRate,
                                 Label lblFirstPayment, Label lblLastPayment)
        {
            try
            {
                double amount = double.Parse(txtAmount.Text);
                double rate = double.Parse(txtRate.Text) / 100;
                int years = int.Parse(txtTerm.Text);
                int months = years * 12;
                double monthlyRate = rate / 12;

                double monthlyPayment = 0;
                double totalPayment = 0;
                double firstPayment = 0;
                double lastPayment = 0;

                if (cmbPaymentType.SelectedIndex == 0) // Аннуитетный
                {
                    monthlyPayment = amount * monthlyRate * Math.Pow(1 + monthlyRate, months) / (Math.Pow(1 + monthlyRate, months) - 1);
                    totalPayment = monthlyPayment * months;
                    firstPayment = monthlyPayment;
                    lastPayment = monthlyPayment;
                }
                else // Дифференцированный
                {
                    double basePrincipal = amount / months;
                    firstPayment = basePrincipal + amount * monthlyRate;
                    lastPayment = basePrincipal + basePrincipal * monthlyRate;

                    totalPayment = 0;
                    double remaining = amount;
                    for (int i = 0; i < months; i++)
                    {
                        double interest = remaining * monthlyRate;
                        totalPayment += basePrincipal + interest;
                        remaining -= basePrincipal;
                    }
                    monthlyPayment = totalPayment / months;
                }

                double overpayment = totalPayment - amount;
                double effectiveRate = (Math.Pow(totalPayment / amount, 1.0 / years) - 1) * 100;

                lblMonthlyPayment.Text = $"Ежемесячный платеж: {monthlyPayment:C0}";
                lblTotalPayment.Text = $"Общая сумма выплат: {totalPayment:C0}";
                lblOverpayment.Text = $"Переплата по кредиту: {overpayment:C0}";
                lblEffectiveRate.Text = $"Эффективная ставка: {effectiveRate:F2}%";
                lblFirstPayment.Text = $"Первый платеж: {firstPayment:C0}";
                lblLastPayment.Text = $"Последний платеж: {lastPayment:C0}";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка расчета: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CalculateGoal(TextBox txtGoalAmount, TextBox txtGoalYears, TextBox txtGoalRate,
                                 Label lblMonthlySave, Label lblTotalSave, Label lblGrowth,
                                 Label lblMonthlyWithout, Label lblEconomy)
        {
            try
            {
                double goalAmount = double.Parse(txtGoalAmount.Text);
                int years = int.Parse(txtGoalYears.Text);
                double rate = double.Parse(txtGoalRate.Text) / 100;

                double monthlyRate = rate / 12;
                int months = years * 12;

                double monthlySave = goalAmount * monthlyRate / (Math.Pow(1 + monthlyRate, months) - 1);
                double totalSaved = monthlySave * months;
                double growth = goalAmount - totalSaved;
                double monthlyWithout = goalAmount / months;
                double economy = monthlyWithout * months - totalSaved;

                lblMonthlySave.Text = $"Ежемесячное пополнение: {monthlySave:C0}";
                lblTotalSave.Text = $"Всего нужно накопить: {totalSaved:C0}";
                lblGrowth.Text = $"Рост от процентов: {growth:C0}";
                lblMonthlyWithout.Text = $"Без инвестиций в месяц: {monthlyWithout:C0}";
                lblEconomy.Text = $"Экономия за счет %: {economy:C0}";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка расчета: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CalculateInvestment(TextBox txtAmount, TextBox txtRate, TextBox txtYears,
                                       Label lblFutureValue, Label lblTotalReturn, Label lblAnnualReturn,
                                       Label lblDoubleTime, Label lblMonthlyGrowth, Label lblROI)
        {
            try
            {
                double amount = double.Parse(txtAmount.Text);
                double rate = double.Parse(txtRate.Text) / 100;
                int years = int.Parse(txtYears.Text);

                double futureValue = amount * Math.Pow(1 + rate, years);
                double totalReturn = futureValue - amount;
                double annualReturn = (Math.Pow(futureValue / amount, 1.0 / years) - 1) * 100;
                double doubleTime = Math.Log(2) / Math.Log(1 + rate);
                double monthlyGrowth = totalReturn / (years * 12);
                double roi = (totalReturn / amount) * 100;

                lblFutureValue.Text = $"Будущая стоимость: {futureValue:C0}";
                lblTotalReturn.Text = $"Общая доходность: {totalReturn:C0}";
                lblAnnualReturn.Text = $"Среднегодовая доходность: {annualReturn:F2}%";
                lblDoubleTime.Text = $"Удвоение капитала за: {doubleTime:F1} лет";
                lblMonthlyGrowth.Text = $"Среднемесячный рост: {monthlyGrowth:C0}";
                lblROI.Text = $"ROI (возврат инвестиций): {roi:F1}%";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка расчета: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CalculatePension(TextBox txtCurrentAge, TextBox txtRetirementAge, TextBox txtMonthlyPension, TextBox txtRate,
                                    Label lblCapital, Label lblMonthlySave, Label lblTotalSave, Label lblDuration,
                                    Label lblWorkingYears, Label lblAnnualPension)
        {
            try
            {
                int currentAge = int.Parse(txtCurrentAge.Text);
                int retirementAge = int.Parse(txtRetirementAge.Text);
                double monthlyPension = double.Parse(txtMonthlyPension.Text);
                double rate = double.Parse(txtRate.Text) / 100;

                int workingYears = retirementAge - currentAge;
                double annualPension = monthlyPension * 12;

                double requiredCapital = annualPension * (1 - Math.Pow(1 + rate, -25)) / rate;

                double monthlyRate = rate / 12;
                int months = workingYears * 12;
                double monthlySave = requiredCapital * monthlyRate / (Math.Pow(1 + monthlyRate, months) - 1);

                double totalSaved = monthlySave * months;

                lblCapital.Text = $"Необходимый капитал: {requiredCapital:C0}";
                lblMonthlySave.Text = $"Ежемесячное пополнение: {monthlySave:C0}";
                lblTotalSave.Text = $"Всего накоплено к пенсии: {totalSaved:C0}";
                lblDuration.Text = $"Пенсионные накопления на: 25 лет";
                lblWorkingYears.Text = $"Лет до пенсии: {workingYears}";
                lblAnnualPension.Text = $"Годовая пенсия: {annualPension:C0}";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка расчета: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}