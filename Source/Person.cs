namespace PracticeConsoleFramework.Source;

internal class Person
{
    public Person(string firstName, string lastName, Sex gender, Handler handler)
    {
        FirstName = firstName;
        LastName = lastName;
        Gender = gender;
        _history = new List<Person>();
        MaturityTrigger = handler;
    }

    private List<Person> _history;

    public string FullName
    {
        get
        {
            var fullName = $"{FirstName} {LastName}";
            if (Gender == Sex.Woman && IsMaried)
                fullName += $" ({_housband?.LastName ?? string.Empty})";
            return fullName;
        }
    }

    public Sex Gender { get; init; }

    public enum Sex
    {
        Woman = 0,
        Man = 1
    }
    public bool IsMaried
    {
        get
        {
            if (_housband is not null)
                return true;
            return _wifes is not null && _wifes.Count > 0;
        }
    }

    private Person? _housband = null;
    private List<Person>? _wifes = null;

    public bool MaryWith(Person person)
    {
        if (_housband is not null) return false;
        if (Gender == Sex.Man && person.Gender == Sex.Woman)
        {
            if (_wifes is not null && _wifes.Contains(person))
                return true;
            _wifes ??= new List<Person>();
            _wifes.Add(person);
            person._housband = this;
            Hit(person);
            person.Hit(this);
            return true;
        }

        return false;
    }

    public Person? Housband => _housband;
    public List<Person>? Wifes => _wifes;
    public bool Divorce()
    {
        if (!IsMaried) return false;
        return _housband is not null && _housband.DivorceFrom(this);
    }
    public bool DivorceFrom(Person person)
    {
        if (Gender == Sex.Woman)
        {
            if (_housband == person)
            {
                _housband = null;
                Hit(person);
                person.Hit(this);
                return true;
            }
        }
        else
        {
            if (_wifes is not null && _wifes.Contains(person))
            {
                _wifes.Remove(person);
                Hit(person);
                person._housband = null;
                person.Hit(this);
                return true;
            }
        }

        return false;
    }

    public static bool operator -(Person first, Person second) => first.DivorceFrom(second);
    public static bool operator +(Person first, Person second) => first.MaryWith(second);

    public string FirstName { get; init; }
    public string LastName { get; init; }

    public delegate int Handler(List<Person> history);

    public Handler MaturityTrigger;
    public int HitCount => _history.Count;

    public Person MostHitter
    {
        get
        {
            Dictionary<Person, int> counter = new();
            foreach (var person in _history)
            {
                if (counter.ContainsKey(person))
                {
                    counter[person]++;
                }
                else
                {
                    counter.Add(person, 1);
                }
            }

            Person? most = null;
            var count = 0;
            foreach (KeyValuePair<Person, int> pair in counter)
            {
                if (most is null)
                {
                    most = pair.Key;
                    count = pair.Value;
                }
                else
                {
                    if (pair.Key != most && pair.Value > count)
                    {
                        most = pair.Key;
                        count = pair.Value;
                    }
                    else
                    {
                        throw new Exception($"Conditional logic Error. key:{pair.Key},most:{most}");
                    }
                }
            }

            if (most == null)
            {
                throw new NullReferenceException("Most is null");
            }

            return most;
        }
    }
    public int Maturity => MaturityTrigger.Invoke(_history);

    public void Hit(Person person)
    {
        _history.Add(person);
    }

    public override string ToString()
    {
        return $"{FullName} is a {Gender}, maturity:{Maturity}, Married:{IsMaried}";
    }
}