Imports System.Data
Imports System.Drawing
Imports System.IO
Imports System.Windows.Forms

Public Class Form1

    Dim products As New DataTable()
    Dim customers As New DataTable()
    Dim sales As New DataTable()

    Dim TAX As Decimal = 0.16D

    Dim txtUser As TextBox
    Dim txtPass As TextBox

    Public Sub New()
        InitializeComponent()
        SetupTables()
        ShowLogin()
    End Sub

    Sub SetupTables()
        If products.Columns.Count = 0 Then
            products.Columns.Add("Name")
            products.Columns.Add("Category")
            products.Columns.Add("Price", GetType(Decimal))
            products.Columns.Add("Stock", GetType(Integer))

            products.Rows.Add("A4 Paper", "Paper", 300, 20)
            products.Rows.Add("Blue Pen", "Pen", 20, 100)
            products.Rows.Add("Exercise Book", "Book", 60, 50)

            customers.Columns.Add("Name")
            customers.Columns.Add("Phone")

            customers.Rows.Add("John Kamau", "0711111111")
            customers.Rows.Add("Mary Wanjiku", "0722222222")

            sales.Columns.Add("Customer")
            sales.Columns.Add("Product")
            sales.Columns.Add("Qty")
            sales.Columns.Add("Total")
        End If
    End Sub

    Sub ClearScreen()
        Me.Controls.Clear()
        Me.Size = New Size(950, 600)
        Me.StartPosition = FormStartPosition.CenterScreen
        Me.BackColor = Color.FromArgb(240, 244, 248)
        Me.Font = New Font("Segoe UI", 10)
    End Sub

    Function MakeButton(text As String, x As Integer, y As Integer, w As Integer, h As Integer, bg As Color) As Button
        Dim btn As New Button()
        btn.Text = text
        btn.Location = New Point(x, y)
        btn.Size = New Size(w, h)
        btn.BackColor = bg
        btn.ForeColor = Color.White
        btn.FlatStyle = FlatStyle.Flat
        btn.FlatAppearance.BorderSize = 0
        btn.Font = New Font("Segoe UI", 10, FontStyle.Bold)
        btn.Cursor = Cursors.Hand
        Return btn
    End Function

    Function MakeTextBox(x As Integer, y As Integer, w As Integer, textValue As String) As TextBox
        Dim txt As New TextBox()
        txt.Location = New Point(x, y)
        txt.Size = New Size(w, 30)
        txt.Font = New Font("Segoe UI", 11)
        txt.Text = textValue
        txt.BorderStyle = BorderStyle.FixedSingle
        Return txt
    End Function

    Function MakeCard(title As String, value As String, x As Integer, y As Integer, bg As Color) As Panel
        Dim card As New Panel()
        card.Location = New Point(x, y)
        card.Size = New Size(170, 100)
        card.BackColor = bg

        Dim lblTitle As New Label()
        lblTitle.Text = title
        lblTitle.ForeColor = Color.White
        lblTitle.Font = New Font("Segoe UI", 10, FontStyle.Bold)
        lblTitle.Location = New Point(15, 15)
        lblTitle.Size = New Size(140, 25)

        Dim lblValue As New Label()
        lblValue.Text = value
        lblValue.ForeColor = Color.White
        lblValue.Font = New Font("Segoe UI", 22, FontStyle.Bold)
        lblValue.Location = New Point(15, 45)
        lblValue.Size = New Size(140, 40)

        card.Controls.Add(lblTitle)
        card.Controls.Add(lblValue)

        Return card
    End Function

    Sub ShowLogin()
        ClearScreen()
        Me.Text = "SmartStation Login"

        Dim leftPanel As New Panel()
        leftPanel.Location = New Point(0, 0)
        leftPanel.Size = New Size(430, 600)
        leftPanel.BackColor = Color.FromArgb(13, 71, 161)

        Dim appTitle As New Label()
        appTitle.Text = "SmartStation"
        appTitle.ForeColor = Color.White
        appTitle.Font = New Font("Segoe UI", 30, FontStyle.Bold)
        appTitle.Location = New Point(55, 170)
        appTitle.Size = New Size(330, 55)

        Dim appSub As New Label()
        appSub.Text = "Stationery Supplies System"
        appSub.ForeColor = Color.WhiteSmoke
        appSub.Font = New Font("Segoe UI", 14)
        appSub.Location = New Point(60, 230)
        appSub.Size = New Size(330, 35)

        Dim welcome As New Label()
        welcome.Text = "Manage products, customers, sales and reports easily."
        welcome.ForeColor = Color.WhiteSmoke
        welcome.Font = New Font("Segoe UI", 11)
        welcome.Location = New Point(60, 285)
        welcome.Size = New Size(300, 80)

        leftPanel.Controls.AddRange({appTitle, appSub, welcome})

        Dim loginCard As New Panel()
        loginCard.Location = New Point(530, 105)
        loginCard.Size = New Size(330, 370)
        loginCard.BackColor = Color.White
        loginCard.BorderStyle = BorderStyle.FixedSingle

        Dim loginTitle As New Label()
        loginTitle.Text = "Login"
        loginTitle.Font = New Font("Segoe UI", 24, FontStyle.Bold)
        loginTitle.ForeColor = Color.FromArgb(13, 71, 161)
        loginTitle.Location = New Point(35, 30)
        loginTitle.Size = New Size(250, 50)

        Dim loginSub As New Label()
        loginSub.Text = "Enter your account details"
        loginSub.ForeColor = Color.Gray
        loginSub.Location = New Point(38, 80)
        loginSub.Size = New Size(250, 25)

        Dim lblUser As New Label()
        lblUser.Text = "Username"
        lblUser.Location = New Point(38, 125)

        txtUser = MakeTextBox(38, 150, 250, "admin")

        Dim lblPass As New Label()
        lblPass.Text = "Password"
        lblPass.Location = New Point(38, 195)

        txtPass = MakeTextBox(38, 220, 250, "1234")
        txtPass.PasswordChar = "*"c

        Dim btnLogin As Button = MakeButton("LOGIN", 38, 275, 250, 42, Color.FromArgb(13, 71, 161))
        AddHandler btnLogin.Click, AddressOf Login

        Dim lblHint As New Label()
        lblHint.Text = "Default login: admin / 1234"
        lblHint.ForeColor = Color.Gray
        lblHint.Location = New Point(38, 325)
        lblHint.Size = New Size(250, 25)
        lblHint.TextAlign = ContentAlignment.MiddleCenter

        loginCard.Controls.AddRange({loginTitle, loginSub, lblUser, txtUser, lblPass, txtPass, btnLogin, lblHint})

        Me.Controls.Add(leftPanel)
        Me.Controls.Add(loginCard)
    End Sub

    Sub Login(sender As Object, e As EventArgs)
        If txtUser.Text = "admin" And txtPass.Text = "1234" Then
            ShowDashboard()
        Else
            MsgBox("Wrong username or password")
        End If
    End Sub

    Sub ShowDashboard()
        ClearScreen()
        Me.Text = "SmartStation Dashboard"

        Dim side As New Panel()
        side.Location = New Point(0, 0)
        side.Size = New Size(210, 600)
        side.BackColor = Color.FromArgb(21, 101, 192)

        Dim logo As New Label()
        logo.Text = "SmartStation"
        logo.ForeColor = Color.White
        logo.Font = New Font("Segoe UI", 18, FontStyle.Bold)
        logo.Location = New Point(22, 30)
        logo.Size = New Size(170, 40)

        Dim small As New Label()
        small.Text = "Stationery System"
        small.ForeColor = Color.WhiteSmoke
        small.Location = New Point(25, 70)
        small.Size = New Size(160, 25)

        Dim btnProducts As Button = MakeButton("Products", 25, 130, 160, 42, Color.FromArgb(25, 118, 210))
        Dim btnCustomers As Button = MakeButton("Customers", 25, 185, 160, 42, Color.FromArgb(25, 118, 210))
        Dim btnSales As Button = MakeButton("Sales", 25, 240, 160, 42, Color.FromArgb(25, 118, 210))
        Dim btnSettings As Button = MakeButton("Settings", 25, 295, 160, 42, Color.FromArgb(25, 118, 210))
        Dim btnLogout As Button = MakeButton("Logout", 25, 500, 160, 42, Color.FromArgb(198, 40, 40))

        AddHandler btnProducts.Click, Sub() ShowProducts()
        AddHandler btnCustomers.Click, Sub() ShowCustomers()
        AddHandler btnSales.Click, Sub() ShowSales()
        AddHandler btnSettings.Click, Sub() ShowSettings()
        AddHandler btnLogout.Click, Sub() ShowLogin()

        side.Controls.AddRange({logo, small, btnProducts, btnCustomers, btnSales, btnSettings, btnLogout})

        Dim header As New Panel()
        header.Location = New Point(210, 0)
        header.Size = New Size(740, 90)
        header.BackColor = Color.White

        Dim title As New Label()
        title.Text = "Dashboard"
        title.Font = New Font("Segoe UI", 24, FontStyle.Bold)
        title.ForeColor = Color.FromArgb(33, 33, 33)
        title.Location = New Point(35, 22)
        title.Size = New Size(300, 45)

        Dim userLabel As New Label()
        userLabel.Text = "Welcome, Admin"
        userLabel.ForeColor = Color.Gray
        userLabel.Font = New Font("Segoe UI", 11)
        userLabel.Location = New Point(560, 35)
        userLabel.Size = New Size(150, 30)

        header.Controls.Add(title)
        header.Controls.Add(userLabel)

        Dim card1 As Panel = MakeCard("Products", products.Rows.Count.ToString(), 250, 130, Color.FromArgb(30, 136, 229))
        Dim card2 As Panel = MakeCard("Customers", customers.Rows.Count.ToString(), 440, 130, Color.FromArgb(67, 160, 71))
        Dim card3 As Panel = MakeCard("Sales", sales.Rows.Count.ToString(), 630, 130, Color.FromArgb(251, 140, 0))

        Dim infoPanel As New Panel()
        infoPanel.Location = New Point(250, 260)
        infoPanel.Size = New Size(610, 230)
        infoPanel.BackColor = Color.White
        infoPanel.BorderStyle = BorderStyle.FixedSingle

        Dim infoTitle As New Label()
        infoTitle.Text = "Quick Guide"
        infoTitle.Font = New Font("Segoe UI", 16, FontStyle.Bold)
        infoTitle.ForeColor = Color.FromArgb(13, 71, 161)
        infoTitle.Location = New Point(25, 20)
        infoTitle.Size = New Size(300, 35)

        Dim infoText As New Label()
        infoText.Text =
            "• Products: add, delete, search and export stationery items." & vbCrLf &
            "• Customers: record customer names and phone numbers." & vbCrLf &
            "• Sales: calculate amount with 16% tax." & vbCrLf &
            "• Settings: change font and background color." & vbCrLf &
            "• Logout: return to login page."

        infoText.Font = New Font("Segoe UI", 11)
        infoText.ForeColor = Color.FromArgb(60, 60, 60)
        infoText.Location = New Point(30, 70)
        infoText.Size = New Size(550, 140)

        infoPanel.Controls.Add(infoTitle)
        infoPanel.Controls.Add(infoText)

        Me.Controls.AddRange({side, header, card1, card2, card3, infoPanel})
    End Sub

    Sub ShowProducts()
        ClearScreen()
        Me.Text = "Products"

        Dim title As New Label()
        title.Text = "Product Management"
        title.Font = New Font("Segoe UI", 22, FontStyle.Bold)
        title.ForeColor = Color.FromArgb(13, 71, 161)
        title.Location = New Point(30, 25)
        title.Size = New Size(400, 45)

        Dim txtName As TextBox = MakeTextBox(30, 105, 170, "")
        Dim txtCategory As TextBox = MakeTextBox(215, 105, 150, "")
        Dim txtPrice As TextBox = MakeTextBox(380, 105, 120, "")
        Dim txtStock As TextBox = MakeTextBox(515, 105, 120, "")

        Me.Controls.AddRange({
            New Label With {.Text = "Product Name", .Location = New Point(30, 80)},
            txtName,
            New Label With {.Text = "Category", .Location = New Point(215, 80)},
            txtCategory,
            New Label With {.Text = "Price", .Location = New Point(380, 80)},
            txtPrice,
            New Label With {.Text = "Stock", .Location = New Point(515, 80)},
            txtStock
        })

        Dim btnAdd As Button = MakeButton("Add", 30, 155, 90, 35, Color.FromArgb(46, 125, 50))
        Dim btnDelete As Button = MakeButton("Delete", 130, 155, 90, 35, Color.FromArgb(198, 40, 40))
        Dim btnSearch As Button = MakeButton("Search", 230, 155, 90, 35, Color.FromArgb(25, 118, 210))
        Dim btnOpen As Button = MakeButton("Open Image", 330, 155, 120, 35, Color.FromArgb(251, 140, 0))
        Dim btnExport As Button = MakeButton("Export", 460, 155, 90, 35, Color.FromArgb(94, 53, 177))
        Dim btnBack As Button = MakeButton("Back", 560, 155, 90, 35, Color.Gray)

        Dim dgv As New DataGridView()
        dgv.Location = New Point(30, 215)
        dgv.Size = New Size(880, 300)
        dgv.DataSource = products
        dgv.AllowUserToAddRows = False
        dgv.BackgroundColor = Color.White

        AddHandler btnAdd.Click, Sub()
                                     Try
                                         If txtName.Text = "" Or txtCategory.Text = "" Then
                                             MsgBox("Enter product name and category")
                                             Exit Sub
                                         End If

                                         products.Rows.Add(txtName.Text, txtCategory.Text, CDec(txtPrice.Text), CInt(txtStock.Text))
                                         MsgBox("Product added")
                                     Catch ex As FormatException
                                         MsgBox("Price and stock must be numbers")
                                     End Try
                                 End Sub

        AddHandler btnDelete.Click, Sub()
                                        If dgv.CurrentRow IsNot Nothing Then
                                            dgv.Rows.Remove(dgv.CurrentRow)
                                        End If
                                    End Sub

        AddHandler btnSearch.Click, Sub()
                                        For Each r As DataRow In products.Rows
                                            If r("Name").ToString().ToLower().Contains(txtName.Text.ToLower()) Then
                                                MsgBox("Found: " & r("Name").ToString())
                                            End If
                                        Next
                                    End Sub

        AddHandler btnOpen.Click, Sub()
                                      Dim ofd As New OpenFileDialog()
                                      ofd.Filter = "Images|*.jpg;*.png;*.jpeg"
                                      If ofd.ShowDialog() = DialogResult.OK Then
                                          MsgBox("Image loaded: " & ofd.FileName)
                                      End If
                                  End Sub

        AddHandler btnExport.Click, AddressOf SaveReport
        AddHandler btnBack.Click, Sub() ShowDashboard()

        AddContextMenu(txtName)

        Me.Controls.AddRange({title, btnAdd, btnDelete, btnSearch, btnOpen, btnExport, btnBack, dgv})
    End Sub

    Sub ShowCustomers()
        ClearScreen()
        Me.Text = "Customers"

        Dim title As New Label()
        title.Text = "Customer Management"
        title.Font = New Font("Segoe UI", 22, FontStyle.Bold)
        title.ForeColor = Color.FromArgb(13, 71, 161)
        title.Location = New Point(30, 25)
        title.Size = New Size(420, 45)

        Dim txtName As TextBox = MakeTextBox(80, 105, 250, "")
        Dim txtPhone As TextBox = MakeTextBox(360, 105, 200, "")

        Dim btnAdd As Button = MakeButton("Add", 80, 155, 90, 35, Color.FromArgb(46, 125, 50))
        Dim btnDelete As Button = MakeButton("Delete", 180, 155, 90, 35, Color.FromArgb(198, 40, 40))
        Dim btnBack As Button = MakeButton("Back", 280, 155, 90, 35, Color.Gray)

        Dim dgv As New DataGridView()
        dgv.Location = New Point(80, 220)
        dgv.Size = New Size(750, 280)
        dgv.DataSource = customers
        dgv.AllowUserToAddRows = False
        dgv.BackgroundColor = Color.White

        AddHandler btnAdd.Click, Sub()
                                     If txtName.Text = "" Or txtPhone.Text = "" Then
                                         MsgBox("Fill all fields")
                                     Else
                                         customers.Rows.Add(txtName.Text, txtPhone.Text)
                                         MsgBox("Customer added")
                                     End If
                                 End Sub

        AddHandler btnDelete.Click, Sub()
                                        If dgv.CurrentRow IsNot Nothing Then
                                            dgv.Rows.Remove(dgv.CurrentRow)
                                        End If
                                    End Sub

        AddHandler btnBack.Click, Sub() ShowDashboard()

        AddContextMenu(txtName)

        Me.Controls.AddRange({
            title,
            New Label With {.Text = "Customer Name", .Location = New Point(80, 80)},
            txtName,
            New Label With {.Text = "Phone", .Location = New Point(360, 80)},
            txtPhone,
            btnAdd, btnDelete, btnBack, dgv
        })
    End Sub

    Sub ShowSales()
        ClearScreen()
        Me.Text = "Sales"

        Dim title As New Label()
        title.Text = "Sales Transaction"
        title.Font = New Font("Segoe UI", 22, FontStyle.Bold)
        title.ForeColor = Color.FromArgb(13, 71, 161)
        title.Location = New Point(280, 40)
        title.Size = New Size(400, 45)

        Dim panel As New Panel()
        panel.Location = New Point(250, 110)
        panel.Size = New Size(430, 330)
        panel.BackColor = Color.White
        panel.BorderStyle = BorderStyle.FixedSingle

        Dim cmbCustomer As New ComboBox()
        cmbCustomer.Location = New Point(150, 50)
        cmbCustomer.Size = New Size(220, 28)

        Dim cmbProduct As New ComboBox()
        cmbProduct.Location = New Point(150, 100)
        cmbProduct.Size = New Size(220, 28)

        Dim txtQty As TextBox = MakeTextBox(150, 150, 220, "")

        For Each r As DataRow In customers.Rows
            cmbCustomer.Items.Add(r("Name").ToString())
        Next

        For Each r As DataRow In products.Rows
            cmbProduct.Items.Add(r("Name").ToString())
        Next

        Dim lblTotal As New Label()
        lblTotal.Text = "Total: Ksh 0"
        lblTotal.Font = New Font("Segoe UI", 14, FontStyle.Bold)
        lblTotal.ForeColor = Color.FromArgb(13, 71, 161)
        lblTotal.Location = New Point(150, 250)
        lblTotal.Size = New Size(250, 35)

        Dim btnCalc As Button = MakeButton("Calculate", 150, 200, 100, 35, Color.FromArgb(25, 118, 210))
        Dim btnSave As Button = MakeButton("Save Sale", 260, 200, 110, 35, Color.FromArgb(46, 125, 50))
        Dim btnBack As Button = MakeButton("Back", 20, 270, 80, 35, Color.Gray)

        Dim totalAmount As Decimal = 0

        AddHandler btnCalc.Click, Sub()
                                      Try
                                          Dim price As Decimal = 0
                                          For Each r As DataRow In products.Rows
                                              If r("Name").ToString() = cmbProduct.Text Then
                                                  price = CDec(r("Price"))
                                              End If
                                          Next

                                          Dim qty As Integer = CInt(txtQty.Text)
                                          Dim subtotal As Decimal = price * qty
                                          Dim taxAmount As Decimal = subtotal * TAX
                                          totalAmount = subtotal + taxAmount

                                          lblTotal.Text = "Total: Ksh " & totalAmount.ToString("N2")
                                      Catch ex As Exception
                                          MsgBox("Enter valid quantity")
                                      End Try
                                  End Sub

        AddHandler btnSave.Click, Sub()
                                      If cmbCustomer.Text = "" Or cmbProduct.Text = "" Then
                                          MsgBox("Select customer and product")
                                      Else
                                          sales.Rows.Add(cmbCustomer.Text, cmbProduct.Text, txtQty.Text, totalAmount)
                                          MsgBox("Sale saved")
                                      End If
                                  End Sub

        AddHandler btnBack.Click, Sub() ShowDashboard()

        panel.Controls.AddRange({
            New Label With {.Text = "Customer", .Location = New Point(40, 50)},
            cmbCustomer,
            New Label With {.Text = "Product", .Location = New Point(40, 100)},
            cmbProduct,
            New Label With {.Text = "Quantity", .Location = New Point(40, 150)},
            txtQty,
            btnCalc, btnSave, btnBack, lblTotal
        })

        Me.Controls.AddRange({title, panel})
    End Sub

    Sub ShowSettings()
        ClearScreen()
        Me.Text = "Settings"

        Dim title As New Label()
        title.Text = "Settings"
        title.Font = New Font("Segoe UI", 24, FontStyle.Bold)
        title.ForeColor = Color.FromArgb(13, 71, 161)
        title.Location = New Point(370, 80)
        title.Size = New Size(250, 50)

        Dim btnFont As Button = MakeButton("Change Font", 360, 170, 200, 45, Color.FromArgb(25, 118, 210))
        Dim btnColor As Button = MakeButton("Change Color", 360, 235, 200, 45, Color.FromArgb(46, 125, 50))
        Dim btnBack As Button = MakeButton("Back", 360, 300, 200, 45, Color.Gray)

        AddHandler btnFont.Click, Sub()
                                      Dim fd As New FontDialog()
                                      If fd.ShowDialog() = DialogResult.OK Then
                                          Me.Font = fd.Font
                                      End If
                                  End Sub

        AddHandler btnColor.Click, Sub()
                                       Dim cd As New ColorDialog()
                                       If cd.ShowDialog() = DialogResult.OK Then
                                           Me.BackColor = cd.Color
                                       End If
                                   End Sub

        AddHandler btnBack.Click, Sub() ShowDashboard()

        Me.Controls.AddRange({title, btnFont, btnColor, btnBack})
    End Sub

    Sub SaveReport(sender As Object, e As EventArgs)
        Dim sfd As New SaveFileDialog()
        sfd.Filter = "Text File|*.txt"
        sfd.FileName = "SmartStationReport.txt"

        If sfd.ShowDialog() = DialogResult.OK Then
            Using sw As New StreamWriter(sfd.FileName)
                sw.WriteLine("SMARTSTATION REPORT")
                sw.WriteLine("Products: " & products.Rows.Count)
                sw.WriteLine("Customers: " & customers.Rows.Count)
                sw.WriteLine("Sales: " & sales.Rows.Count)
            End Using
            MsgBox("File saved")
        End If
    End Sub

    Sub AddContextMenu(tb As TextBox)
        Dim cms As New ContextMenuStrip()

        cms.Items.Add("Copy", Nothing, Sub()
                                           If tb.Text <> "" Then
                                               Clipboard.SetText(tb.Text)
                                               MsgBox("Copied")
                                           Else
                                               MsgBox("Nothing to copy")
                                           End If
                                       End Sub)

        cms.Items.Add("Paste", Nothing, Sub()
                                            If Clipboard.ContainsText() Then
                                                tb.Text = Clipboard.GetText()
                                            Else
                                                MsgBox("Clipboard empty")
                                            End If
                                        End Sub)

        cms.Items.Add("Clear", Nothing, Sub()
                                            tb.Clear()
                                        End Sub)

        tb.ContextMenuStrip = cms
    End Sub

End Class