namespace PracticeConsoleFramework.Source.Practice;

internal class PrcPerson : IClass
{
    public PrcPerson()
    {
        PrcName = "The Person at the time";
        _personList = new List<Person>();
    }

    public string PrcName { get; init; }
    private readonly List<Person> _personList;

    public void Trigger()
    {
        Intro();

        Control();
    }

    private void Intro()
    {
        Write("Welcome to person town");
        Write("\nYou must create person and doing something with your persons.");
        ReadLine();
    }

    private void Control()
    {
    firstStep:
        Clear();
        Write("Do you like:");
        Write("\n1.Define new person");
        Write("\n2.Mary with");
        Write("\n3.Divorce From");
        Write("\n4.Show person list");
        Write("\n0.Exit Practice");
    WitchOne:
        Write("\nWhich one?");
        var result = ReadLine();
        if (string.IsNullOrEmpty(result)) goto WitchOne;
        Clear();
        switch (result)
        {
            case "1":
                GetPerson();
                break;
            case "2":
                MarriageOperation();
                break;
            case "3":
                DivorceOperation();
                break;
            case "4":
                ShowPersons(true);
                break;
            case "0":
                return;
        }
        goto firstStep;
    }

    private void DivorceOperation(bool cleanPage = false)
    {
    firstStep:
        if (cleanPage) Clear();
        var list = _personList.Where(x => x.IsMaried).ToList();
        if (list.Any() == false) return;
        ShowPersons(list);
        Write("\nWhich one need divorce?");
        var readLine = ReadLine();
        if (string.IsNullOrEmpty(readLine))
        {
            Write("\nplease enter index of person!");
            ReadLine();
            goto firstStep;
        }

        if (int.TryParse(readLine, out int pressedIndex))
        {
            pressedIndex--;
            if (pressedIndex < list.Count || pressedIndex > 0)
            {
                if (!list[pressedIndex].IsMaried)
                {
                    Write("\nPlease which one married!");
                    ReadLine();
                    goto firstStep;
                }

                if (list[pressedIndex].Gender == Person.Sex.Man)
                {
                    var wife = _personList.Where(x => x.Housband == list[pressedIndex]).ToList();
                    if (wife.Count > 0)
                    {
                        Write($"\n {list[pressedIndex].FullName} Wife list:");
                        ShowPersons(wife);
                    DivorceWife:
                        Write("\n\tWhich wife you want divorce:");
                        var wifeIndex = ReadLine();
                        if (string.IsNullOrEmpty(wifeIndex))
                        {
                            Write("You are nothing to do, bye!");
                            return;
                        }

                        if (int.TryParse(wifeIndex, out int index))
                        {
                            index--;
                            if (index > wife.Count || index < 0)
                            {
                                Write("\n\tpick correct!");
                                ReadLine();
                                goto DivorceWife;
                            }

                            if (wife[index].Divorce())
                                Write("\nDivorced!");
                            else
                                Write("\nDivorcing is fail!");
                            ReadLine();
                        }
                        else
                        {
                            Write("\n\tpick correct!");
                            ReadLine();
                            goto DivorceWife;
                        }
                    }

                }

                if (list[pressedIndex].Gender == Person.Sex.Woman)
                {
                    if (list[pressedIndex].Divorce())
                        Write("\nDivorced!");
                    else
                        Write("\nDivorcing is fail!");
                    ReadLine();
                }
            }
            else
            {
                Write("\nplease enter index of person!");
                ReadLine();
                goto firstStep;
            }
        }
        else
        {
            Write("\nplease enter index of person!");
            ReadLine();
            goto firstStep;
        }
    }

    private void MarriageOperation()
    {
        ShowPersons();
        if (_personList.Count < 2)
        {
            Write("\nfor marriage operation you need at least two person");
            ReadLine();
            return;
        }
    getArgument:
        Write("\n\nSelect First and Second Person to marry with together(FirstPersonIndex,SecondPersonIndex):");
        var readLine = ReadLine();
        if (string.IsNullOrEmpty(readLine))
        {
            Write("\nMarriage operation is fail! command is null or empty.");
            ReadLine();
            return;
        }

        var split = readLine.Split(',');
        if (split.Length != 2)
        {
            Write("\nWrite Correct Syntax! like 1,2");
            ReadLine();
            goto getArgument;
        }

        if (int.TryParse(split[0], out int firstPerson))
        {
            if (firstPerson > _personList.Count || firstPerson < 1)
            {
                Write("\nFirst person index is out of range.");
                ReadLine();
                goto getArgument;
            }
            if (int.TryParse(split[1], out int secondPerson))
            {
                if (secondPerson > _personList.Count || secondPerson < 1)
                {
                    Write("\nSecond person index is out of range.");
                    ReadLine();
                    goto getArgument;
                }
                if (firstPerson == secondPerson)
                {
                    Write("\nYou cant marry with your self, please choose different person.");
                    ReadLine();
                    goto getArgument;
                }

                var first = _personList[firstPerson - 1];
                var second = _personList[secondPerson - 1];
                if (first.MaryWith(second))
                    Write($"\n{first.FirstName} is married to {second.FullName}");
                else
                    Write("\nMarriage fail!");
                ReadLine();
            }
        }
    }

    private void ShowPersons(bool needPause = false) => ShowPersons(_personList, needPause);
    private void ShowPersons(List<Person> list, bool needPause = false)
    {
        Write("Person List");
        if (list.Count < 1) return;
        for (int i = 0; i < list.Count; i++)
        {
            Write($"\n{i + 1}.{list[i]}");
        }
        if (needPause)
            Read();
    }
    private Person GetPerson()
    {
        Write("\nEnter person information:");
        Write("\n\tEnter FirstName:");
    stepOne:
        var firstName = ReadLine();
        if (string.IsNullOrEmpty(firstName)) goto stepOne;

        stepTwo:
        Write("\n\tEnter LastName:");
        var lastName = ReadLine();
        if (string.IsNullOrEmpty(lastName)) goto stepTwo;

        stepThree:
        Write("\n\tEnter Sex(man,woman):");
        var gender = ReadLine();
        if (string.IsNullOrEmpty(gender)) goto stepThree;
        if (!(gender.Equals("man") || gender.Equals("woman")))
            goto stepThree;
        var sex = gender switch
        {
            "man" => Person.Sex.Man,
            "woman" => Person.Sex.Woman,
            _ => throw new ArgumentOutOfRangeException()
        };

        Person prc = new(firstName, lastName, sex, MaturityCalculator);
        _personList.Add(prc);
        return prc;
    }

    private int MaturityCalculator(List<Person> history)
    {
        return history.Count;
    }
}