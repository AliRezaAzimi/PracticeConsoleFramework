using System.IO.Enumeration;

namespace PracticeConsoleFramework.Source;

internal class Employee : Person
{
    private Employee(Person person) : base(person.FirstName, person.LastName, person.Gender, person.MaturityTrigger)
    {

    }

    public static Employee CreateInstance(Person person)
    {
        return new Employee(person);
    }

    public DateTime? ToBeHiredTime { get; internal set; }
    public Employee? Employer { get; internal set; }
    public List<Employee>? Employees { get; internal set; }
    public bool IsEmployee => Employer is not null;
    public bool IsEmployer => Employees is not null && Employees.Count > 0;

    public bool ContainEmployee(Employee employee)
    => Employees is not null ? Employees.Contains(employee) : false;

    public bool Hiring(Employee employee)
    {
        if (!employee.IsEmployee)
        {
            if (employee.Hired(this))
            {
                Employees ??= new List<Employee>();
                Employees.Add(employee);
            }
        }

        return false;
    }

    public bool Hired(Employee employee)
    {
        if (!employee.IsEmployee) return false;
        Employer = employee;
        ToBeHiredTime = DateTime.Now;
        return true;
    }

    private Dictionary<Employee, decimal>? _paymentBalance;
    public decimal PayWith(Employee employee, TimeSpan workTime)
    {
        _paymentBalance ??= new Dictionary<Employee, decimal>();

        decimal salary = new decimal(workTime.TotalSeconds * 10);

        if (_paymentBalance.ContainsKey(employee))
        {
            _paymentBalance[employee] += salary;
        }
        else
        {
            _paymentBalance.Add(employee, salary);
        }

        return salary;
    }

    public decimal PayOnExit(Employee employee)
    {
        var payment = _paymentBalance?.ContainsKey(employee) ?? false
            ? _paymentBalance[employee] > 0 ? _paymentBalance[employee] * 0.2M : 0
            : 0;
        _paymentBalance?.Remove(employee);
        return payment;
    }

    public bool Fire(Employee employee)
    {
        if ((Employees?.Contains(employee) ?? false) && employee.Fired())
        {
            Employees.Remove(employee);
            _paymentBalance?.Remove(employee);
            return true;
        }

        return false;
    }

    public bool Fired()
    {
        if (IsEmployee)
        {
            Employer = null;
            ToBeHiredTime = null;
        }

        return false;
    }
}