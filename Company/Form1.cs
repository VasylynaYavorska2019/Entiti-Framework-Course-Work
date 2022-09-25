using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity.Validation;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace Company
{
    public partial class Form1 : Form
    {
        private readonly Project1Entities db;
      
        public Form1()
        {
            InitializeComponent();
            db = new Project1Entities();
            RefreshForm();


        }
        private void RefreshForm()
        { 
            passport_data_datagridview.DataSource = db.Pasport_data.ToList();
            employee_datagridview.DataSource = db.Employees.ToList();
            passport_id__employee.DataSource = db.Pasport_data.ToList(); 
            passport_id__employee.DisplayMember = "passport_id";
            position_id_employee.DataSource = db.Positions.ToList();
            position_id_employee.DisplayMember = "position_id";
            department_id_employee.DataSource = db.Departments.ToList();
            department_id_employee.DisplayMember = "department_id";
            vacation_datagridview.DataSource = db.Vacations.ToList();
            employeee_id_vacation.DataSource = db.Employees.ToList();
            employeee_id_vacation.DisplayMember = "employee_id";
            working_hours_datagridview.DataSource = db.WorkingHours.ToList();
            employee_id_working_hours.DataSource = db.Employees.ToList();
            employee_id_working_hours.DisplayMember = "employee_id";
            salary_issuance_datagridview.DataSource = db.Salary_issuance.ToList();
            id_salary_issuance.DataSource = db.WorkingHours.ToList();
            id_salary_issuance.DisplayMember = "Id";
            department_datagridview.DataSource = db.Departments.ToList();
            leader_id_department.DataSource = db.Employees.ToList();
            leader_id_department.DisplayMember = "employee_id";
            position_datagridview.DataSource = db.Positions.ToList();
            department_id_position.DataSource = db.Departments.ToList();
            department_id_position.DisplayMember = "department_id";

        }

            private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void add_passport_data_Click(object sender, EventArgs e)
        {
            if(surname_passport_data.Text!=""&& name_passport_data.Text != "" && address_passport_data.Text!="" && place_passport_data.Text!="")
            {
                try
                {

                    Pasport_data pass = new Pasport_data();
                    pass.surnane = surname_passport_data.Text;
                    pass.name = name_passport_data.Text;
                    pass.adress = address_passport_data.Text;
                    pass.plase = place_passport_data.Text;
                    pass.birtdate = birthdate_passport_data.Value;
                    var idval = new Random();
                    pass.passport_id = idval.Next(0, 999999).ToString();
                    db.Pasport_data.Add(pass);
                    db.SaveChanges();
                    RefreshForm();
                }
                catch (DbEntityValidationException ex)
                {
                    foreach (var eve in ex.EntityValidationErrors)
                    {
                        Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                            eve.Entry.Entity.GetType().Name, eve.Entry.State);
                        foreach (var ve in eve.ValidationErrors)
                        {
                            Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                                ve.PropertyName, ve.ErrorMessage);
                        }
                    }
                    throw;
                }
            }
            else { MessageBox.Show("fill in all fields, please"); }
        }

        private void update_passport_data_Click(object sender, EventArgs e)
        {
            if (passport_data_datagridview.SelectedRows.Count > 0)
            {
                int index = passport_data_datagridview.SelectedRows[0].Index;
                int id = 0;
                bool converted = Int32.TryParse(passport_data_datagridview[0, index].Value.ToString(), out id);
                if (converted == false)
                    return;
                var id1 = id.ToString();
                Pasport_data pass = db.Pasport_data.Find(id1);
                 pass.surnane= surname_passport_data.Text;
                 pass.name =name_passport_data.Text;
                 pass.adress= address_passport_data.Text;
                 pass.plase = place_passport_data.Text ;
                 pass.birtdate = birthdate_passport_data.Value;

                db.SaveChanges();
                RefreshForm();


            }

        }

        private void delete_passport_data_Click(object sender, EventArgs e)
        {
            if (passport_data_datagridview.SelectedRows.Count > 0)
            {
                int index = passport_data_datagridview.SelectedRows[0].Index;
                int id = 0;
                bool converted = Int32.TryParse(passport_data_datagridview[0, index].Value.ToString(), out id);
                if (converted == false)
                    return;
                var id1 = id.ToString();
                Pasport_data pass = db.Pasport_data.Find(id1);

                db.Pasport_data.Remove(pass);
                //DELETING CHILD EMPLOEE RECORDS ALSO
                var childs = db.Employees.Where(c => c.passport_id == id1);
                foreach (var item in childs)
                {
                
                       
                        var childsVac = db.Vacations.Where(c => c.employee_id == item.employee_id);
                        foreach (var item1 in childsVac)
                        {

                            db.Vacations.Remove(item1);
                        }
                        var childsHours = db.WorkingHours.Where(c => c.employee_id == item.employee_id);
                        foreach (var item2 in childsHours)
                        {
                            var childsSI = db.Salary_issuance.Where(c => c.Id == item2.Id);
                            foreach (var item3 in childsSI)
                            {

                                db.Salary_issuance.Remove(item3);
                            }
                        db.WorkingHours.Remove(item2);

                        }
                     db.Employees.Remove(item);

                }

                db.SaveChanges();
                    RefreshForm();


              
            }
        }
        private void edit_passport_data_Click(object sender, EventArgs e)
        {
            if (passport_data_datagridview.SelectedRows.Count > 0)
            {
                int index = passport_data_datagridview.SelectedRows[0].Index;
                int id = 0;
                bool converted = Int32.TryParse(passport_data_datagridview[0, index].Value.ToString(), out id);
                if (converted == false)
                    return;
                var id1 = id.ToString();
                Pasport_data pass = db.Pasport_data.Find(id1);
                surname_passport_data.Text = pass.surnane; 
                name_passport_data.Text = pass.name;
                address_passport_data.Text = pass.adress;
                place_passport_data.Text = pass.plase;
                birthdate_passport_data.Value = (DateTime)pass.birtdate;


            }
        }

        private void add_employee_Click(object sender, EventArgs e)
        {
            Employee emp = new Employee();
            emp.passport_id = passport_id__employee.Text;
            emp.position_id = position_id_employee.Text;
            emp.department_id = department_id_employee.Text;
            emp.start_work = start_work_employee.Value;
            emp.end_work = end_work_employee.Value;
            if ((DateTime)emp.start_work > (DateTime)emp.end_work)
            {
                MessageBox.Show("enter corect data values, please");
            }
            else
            {
                var idval = new Random();
                emp.employee_id = idval.Next(20, 99).ToString();

                db.Employees.Add(emp);
                db.SaveChanges();
                RefreshForm();
            }
        }

        private void edit_employee_Click(object sender, EventArgs e)
        {
            if (employee_datagridview.SelectedRows.Count > 0)
            {
                int index = employee_datagridview.SelectedRows[0].Index;
                int id = 0;
                bool converted = Int32.TryParse(employee_datagridview[0, index].Value.ToString(), out id);
                if (converted == false)
                    return;
                var id1 = id.ToString();
                Employee emp = db.Employees.Find(id1);
                passport_id__employee.Text = emp.passport_id;
                position_id_employee.Text = emp.position_id;
                department_id_employee.Text = emp.department_id;
                start_work_employee.Value = (DateTime)emp.start_work;
               // end_work_employee.Value = (DateTime)emp.end_work;


            }

        }

        private void update_employee_Click(object sender, EventArgs e)
        {
            if (employee_datagridview.SelectedRows.Count > 0)
            {
                int index = employee_datagridview.SelectedRows[0].Index;
                int id = 0;
                bool converted = Int32.TryParse(employee_datagridview[0, index].Value.ToString(), out id);
               

                var id1 = id.ToString();
                Employee emp = db.Employees.Find(id1);
                emp.passport_id = passport_id__employee.Text;
                emp.position_id = position_id_employee.Text;
                emp.department_id = department_id_employee.Text;
                emp.start_work = start_work_employee.Value;
                emp.end_work = end_work_employee.Value;
                if ((DateTime)emp.start_work > (DateTime)emp.end_work)
                {
                    MessageBox.Show("enter corect data values, please");
                }
                else
                {
                    db.SaveChanges();
                    RefreshForm();
                }


            }
        }

        private void delete_employee_Click(object sender, EventArgs e)
        {
            if (employee_datagridview.SelectedRows.Count > 0)
            {
                int index = employee_datagridview.SelectedRows[0].Index;
                int id = 0;
                bool converted = Int32.TryParse(employee_datagridview[0, index].Value.ToString(), out id);
                if (converted == false)
                    return;
                var id1 = id.ToString();
                Employee emp = db.Employees.Find(id1);

                db.Employees.Remove(emp);
                //DELETING CHILD VACATIONS  AND WORKING HOURS RECORDS ALSO
                var childsVac = db.Vacations.Where(c => c.employee_id == id1);
                foreach (var item in childsVac)
                {

                    db.Vacations.Remove(item);
                }
                var childsHours = db.WorkingHours.Where(c => c.employee_id == id1);
                foreach (var item in childsHours)
                {
                    var childsSI = db.Salary_issuance.Where(c => c.Id == item.Id);
                    foreach (var item1 in childsSI)
                    {

                        db.Salary_issuance.Remove(item1);
                    }

                    db.WorkingHours.Remove(item);
                    
                }

                db.SaveChanges();
                RefreshForm();

            }
        }

        private void add_vacation_Click(object sender, EventArgs e)
        {
            Vacation vac = new Vacation();
            vac.employee_id = employeee_id_vacation.Text;
            vac.vacation_start = vacation_start.Value;
            vac.vacation_end = vacation_end.Value;
            if ((DateTime)vac.vacation_start > (DateTime)vac.vacation_end)
            {
                MessageBox.Show("enter corect data values, please");
            }
            else
            {
                var idval = new Random();
                vac.vacation_id = idval.Next(0, 99).ToString();
                db.Vacations.Add(vac);
                db.SaveChanges();
                RefreshForm();
            }
        }

        private void delete_vacation_Click(object sender, EventArgs e)
        {
            if (vacation_datagridview.SelectedRows.Count > 0)
            {
                int index = vacation_datagridview.SelectedRows[0].Index;
                int id = 0;
                bool converted = Int32.TryParse(vacation_datagridview[0, index].Value.ToString(), out id);
                if (converted == false)
                    return;
                var id1 = id.ToString();
                Vacation vac = db.Vacations.Find(id1);

                db.Vacations.Remove(vac);
                


                db.SaveChanges();
                RefreshForm();
             }
 
            }

        private void edit_vacation_Click(object sender, EventArgs e)
        {
            if (vacation_datagridview.SelectedRows.Count > 0)
            {
                int index = vacation_datagridview.SelectedRows[0].Index;
                int id = 0;
                bool converted = Int32.TryParse(vacation_datagridview[0, index].Value.ToString(), out id);
                if (converted == false)
                    return;
                var id1 = id.ToString();
                Vacation vac = db.Vacations.Find(id1);
                employeee_id_vacation.Text = vac.employee_id;
                vacation_start.Value = (DateTime)vac.vacation_start;
                vacation_end.Value= (DateTime)vac.vacation_end;


            }
        }

        private void update_vacation_Click(object sender, EventArgs e)
        {
            if (vacation_datagridview.SelectedRows.Count > 0)
            {
                int index = vacation_datagridview.SelectedRows[0].Index;
                int id = 0;
                bool converted = Int32.TryParse(vacation_datagridview[0, index].Value.ToString(), out id);


                var id1 = id.ToString();
                Vacation vac = db.Vacations.Find(id1);
                vac.employee_id = employeee_id_vacation.Text;
                vac.vacation_start = vacation_start.Value;
                vac.vacation_end = vacation_end.Value;
                if ((DateTime)vac.vacation_start > (DateTime)vac.vacation_end)
                {
                    MessageBox.Show("enter corect data values, please");
                }
                else
                {
                    db.SaveChanges();
                    RefreshForm();
                }
                
            }

            }

        private void hours_count_working_hours_TextChanged(object sender, EventArgs e)
        {

        }

        private void hours_count_working_hours_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
        }

        private void add_working_hours_Click(object sender, EventArgs e)
        {
            if (hours_count_working_hours.Text != "" && overtime_hours_count_working_hours.Text != "" && day_off_hours_count_working_hours.Text != "")
            {

                WorkingHour wh = new WorkingHour();
                wh.employee_id = employee_id_working_hours.Text;
                wh.hour_count = Convert.ToInt32(hours_count_working_hours.Text);
                wh.overtime_hours_count = Convert.ToInt32(overtime_hours_count_working_hours.Text);
                wh.day_off_hours_count = Convert.ToInt32(day_off_hours_count_working_hours.Text);
                wh.salary_date = salary_data_working_hours.Value;
                int idval = db.WorkingHours.Max(p => p.Id); idval += 1;
                wh.Id = idval;
                db.WorkingHours.Add(wh);
                db.SaveChanges();
                RefreshForm();
            }
            else { MessageBox.Show("fill in all fields, please"); }
        }

        private void edit_working_hours_Click(object sender, EventArgs e)
        {
            if (working_hours_datagridview.SelectedRows.Count > 0)
            {
                int index = working_hours_datagridview.SelectedRows[0].Index;
                int id = 0;
                bool converted = Int32.TryParse(working_hours_datagridview[0, index].Value.ToString(), out id);
                if (converted == false)
                    return;
               // var id1 = id.ToString();
                WorkingHour wh = db.WorkingHours.Find(id);
                employee_id_working_hours.Text = wh.employee_id;
                hours_count_working_hours.Text = wh.hour_count.ToString();
                overtime_hours_count_working_hours.Text = wh.overtime_hours_count.ToString();
                day_off_hours_count_working_hours.Text = wh.day_off_hours_count.ToString();
                salary_data_working_hours.Value = (DateTime)wh.salary_date;


            }
        }

        private void update_working_hours_Click(object sender, EventArgs e)
        {
            if (working_hours_datagridview.SelectedRows.Count > 0)
            {
                int index = working_hours_datagridview.SelectedRows[0].Index;
                int id = 0;
                bool converted = Int32.TryParse(working_hours_datagridview[0, index].Value.ToString(), out id);


               // var id1 = id.ToString();
                WorkingHour wh = db.WorkingHours.Find(id);
                wh.employee_id = employee_id_working_hours.Text;
                wh.hour_count = Convert.ToInt32(hours_count_working_hours.Text);
                wh.overtime_hours_count = Convert.ToInt32(overtime_hours_count_working_hours.Text);
                wh.day_off_hours_count = Convert.ToInt32(day_off_hours_count_working_hours.Text);
                wh.salary_date = salary_data_working_hours.Value;

                db.SaveChanges();
                RefreshForm();


            }
        
        }

        private void delete_working_hours_Click(object sender, EventArgs e)
        {

            if (working_hours_datagridview.SelectedRows.Count > 0)
            {
                int index = working_hours_datagridview.SelectedRows[0].Index;
                int id = 0;
                bool converted = Int32.TryParse(working_hours_datagridview[0, index].Value.ToString(), out id);
                if (converted == false)
                    return;
                
                WorkingHour wh = db.WorkingHours.Find(id);

                db.WorkingHours.Remove(wh);
                //DELETING CHILD SALARY ISSURANCE RECORDS ALSO
                var childsSI = db.Salary_issuance.Where(c => c.Id == id);
                foreach (var item in childsSI)
                {

                    db.Salary_issuance.Remove(item);
                }

                db.SaveChanges();
                RefreshForm();

            }
        }

        private void label19_Click(object sender, EventArgs e)
        {

        }

        private void add_salary_issuance_Click(object sender, EventArgs e)
        {
            Salary_issuance si = new Salary_issuance();
            si.Id = Convert.ToInt32(id_salary_issuance.Text);
            si.salary_day = salary_day_salary_issues.Value;
            si.planed_salary_day = planed_salary_day_salary_issues.Value;
            var idval = new Random(); 
            si.salary_id = idval.Next(0, 99).ToString();
            db.Salary_issuance.Add(si);
            db.SaveChanges();
            RefreshForm();
        }

        private void edit_salary_issuance_Click(object sender, EventArgs e)
        {

            if (salary_issuance_datagridview.SelectedRows.Count > 0)
            {
                int index = salary_issuance_datagridview.SelectedRows[0].Index;
                int id = 0;
                bool converted = Int32.TryParse(salary_issuance_datagridview[0, index].Value.ToString(), out id);
                if (converted == false)
                    return;

                Salary_issuance si = db.Salary_issuance.Find(id.ToString());
                id_salary_issuance.Text = si.Id.ToString();
                salary_day_salary_issues.Value = (DateTime)si.salary_day;
                planed_salary_day_salary_issues.Value = (DateTime)si.planed_salary_day;


            }
        }

        private void update_salary_issuance_Click(object sender, EventArgs e)
        {
            if (salary_issuance_datagridview.SelectedRows.Count > 0)
            {
                int index = salary_issuance_datagridview.SelectedRows[0].Index;
                int id = 0;
                bool converted = Int32.TryParse(salary_issuance_datagridview[0, index].Value.ToString(), out id);


                // var id1 = id.ToString();
                Salary_issuance si = db.Salary_issuance.Find(id.ToString());
                si.Id = Convert.ToInt32(id_salary_issuance.Text);
                si.salary_day = salary_day_salary_issues.Value;
                si.planed_salary_day = planed_salary_day_salary_issues.Value;

                db.SaveChanges();
                RefreshForm();
                

            }
        }

        private void delete_salary_issuance_Click(object sender, EventArgs e)
        {

            if (salary_issuance_datagridview.SelectedRows.Count > 0)
            {
                int index = salary_issuance_datagridview.SelectedRows[0].Index;
                int id = 0;
                bool converted = Int32.TryParse(salary_issuance_datagridview[0, index].Value.ToString(), out id);
                if (converted == false)
                    return;

                Salary_issuance si = db.Salary_issuance.Find(id.ToString());

                db.Salary_issuance.Remove(si);
               
                db.SaveChanges();
                RefreshForm();

            }
        }

        private void add_department_Click(object sender, EventArgs e)
        {
            if (title_department.Text != "" && department_id_department.Text!="")
            {
                try
                {
                    Department dep = new Department();
                    dep.department_leader_id = leader_id_department.Text;
                    dep.departmen_title = title_department.Text;
                    dep.department_id = department_id_department.Text;
                    db.Departments.Add(dep);
                    db.SaveChanges();
                    RefreshForm();
                }
                catch
                {
                    MessageBox.Show("department id has to be unique");
                }
                
            }
            else { MessageBox.Show("fill in all fields, please"); }
        }

        private void edit_department_Click(object sender, EventArgs e)
        {
            if (department_datagridview.SelectedRows.Count > 0)
            {
                int index = department_datagridview.SelectedRows[0].Index;
                string id = department_datagridview[0, index].Value.ToString();


                Department dep = db.Departments.Find(id);
               
                title_department.Text = dep.departmen_title;
                leader_id_department.Text = dep.department_leader_id;
                department_id_department.Enabled = false;

            }
        }

        private void update_department_Click(object sender, EventArgs e)
        {

            if (department_datagridview.SelectedRows.Count > 0)
            {
                int index = department_datagridview.SelectedRows[0].Index;
                string id = department_datagridview[0, index].Value.ToString();

                if (title_department.Text== "")
                {
                    MessageBox.Show("fill in all fields, please");
                }
               
                    Department dep = db.Departments.Find(id);
                dep.department_leader_id = leader_id_department.Text;
                dep.departmen_title = title_department.Text;

                db.SaveChanges();
                RefreshForm();
                


            }
        }

        private void overtime_hours_count_working_hours_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
        }

        private void day_off_hours_count_working_hours_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
        }

        private void department_id_department_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
        }

        private void delete_department_Click(object sender, EventArgs e)
        {
            if (department_datagridview.SelectedRows.Count > 0)
            {
                int index = department_datagridview.SelectedRows[0].Index;
                string id = department_datagridview[0, index].Value.ToString();

                Department dep = db.Departments.Find(id);

                db.Departments.Remove(dep);
                //DELETING CHILD POSITION AND EMPLOYEE RECORDS ALSO
                var childsSI = db.Positions.Where(c => c.department_id.ToString() == id);
                foreach (var item in childsSI)
                {

                    db.Positions.Remove(item);
                }
                //DELETING CHILD EMPLOEE RECORDS ALSO with its childrens
                var childs = db.Employees.Where(c => c.passport_id.ToString() == id);
                foreach (var item in childs)
                {
                    var childsVac = db.Vacations.Where(c => c.employee_id == item.employee_id);
                    foreach (var item1 in childsVac)
                    {

                        db.Vacations.Remove(item1);
                    }
                    var childsHours = db.WorkingHours.Where(c => c.employee_id == item.employee_id);
                    foreach (var item2 in childsHours)
                    {
                        var childs1 = db.Salary_issuance.Where(c => c.Id == item2.Id);
                        foreach (var item3 in childs1)
                        {

                            db.Salary_issuance.Remove(item3);
                        }
                        db.WorkingHours.Remove(item2);

                    }
                    db.Employees.Remove(item);

                }

                db.SaveChanges();
                RefreshForm();

            }
        }

        private void tabPage7_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            //EDIT POSITION BEFORE BUTTON RENAMING
            if (position_datagridview.SelectedRows.Count > 0)
            {
                int index = position_datagridview.SelectedRows[0].Index;
                string id = position_datagridview[0, index].Value.ToString();


                Position pos = db.Positions.Find(id);
                position_id_position.Enabled = false;
                department_id_position.Text = pos.department_id;
                position_title_position.Text = pos.position_title;
                payment_per_hour_position.Text = pos.payment_per_hour.ToString();
                vacation_value_position.Text = pos.vacation_value.ToString();
            }

        }

        private void add_position_Click(object sender, EventArgs e)
        {
            if (position_id_position.Text != "" && position_title_position.Text != "")
            {
                try
                {
                    Position pos = new Position();
                    pos.position_id = position_id_position.Text;
                    pos.department_id = department_id_position.Text;
                    pos.position_title = position_title_position.Text;
                    pos.payment_per_hour =Convert.ToDecimal(payment_per_hour_position.Text);
                    try
                    {
                        if (vacation_value_position.Text!="") {
                            pos.vacation_value = Convert.ToInt32(vacation_value_position.Text);
                        }
                       
                    }
                    catch
                    {
                        MessageBox.Show("choose 0|1 value");
                    }
                    
                    db.Positions.Add(pos);
                    db.SaveChanges();
                    RefreshForm();
                }
                catch
                {
                    MessageBox.Show("position id has to be unique");
                }

            }
            else { MessageBox.Show("fill in all fields, please"); }
        }

        private void payment_per_hour_position_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
        }

        private void update_position_Click(object sender, EventArgs e)
        {
            if (position_datagridview.SelectedRows.Count > 0)
            {
                int index = position_datagridview.SelectedRows[0].Index;
                string id = position_datagridview[0, index].Value.ToString();

                if (position_title_position.Text == "")
                {
                    MessageBox.Show("fill in all fields, please");
                }
               
                    Position pos = db.Positions.Find(id);
                    pos.department_id = department_id_position.Text;
                    pos.position_title = position_title_position.Text;
                    pos.payment_per_hour = Convert.ToDecimal(payment_per_hour_position.Text);
                    try
                    {
                        if (vacation_value_position.Text != "")
                        {
                            pos.vacation_value = Convert.ToInt32(vacation_value_position.Text);
                        }

                    }
                    catch
                    {
                        MessageBox.Show("choose 0|1 value");
                    }

                db.SaveChanges();
                    RefreshForm();
               


            }
        }

        private void delete_position_Click(object sender, EventArgs e)
        {
            if (position_datagridview.SelectedRows.Count > 0)
            {
                int index = position_datagridview.SelectedRows[0].Index;
                string id = position_datagridview[0, index].Value.ToString();

                Position pos = db.Positions.Find(id);

                db.Positions.Remove(pos);
                
                //DELETING CHILD EMPLOEE RECORDS ALSO with its childrens
                var childs = db.Employees.Where(c => c.passport_id.ToString() == id);
                foreach (var item in childs)
                {
                    var childsVac = db.Vacations.Where(c => c.employee_id == item.employee_id);
                    foreach (var item1 in childsVac)
                    {

                        db.Vacations.Remove(item1);
                    }
                    var childsHours = db.WorkingHours.Where(c => c.employee_id == item.employee_id);
                    foreach (var item2 in childsHours)
                    {
                        var childs1 = db.Salary_issuance.Where(c => c.Id == item2.Id);
                        foreach (var item3 in childs1)
                        {

                            db.Salary_issuance.Remove(item3);
                        }
                        db.WorkingHours.Remove(item2);

                    }
                    db.Employees.Remove(item);

                }

                db.SaveChanges();
                RefreshForm();

            }
        }
    }
}
