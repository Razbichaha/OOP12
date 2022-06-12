using System;
using System.Collections.Generic;

namespace OOP12
{
    class Program
    {
        static void Main(string[] args)
        {
            ProgramCore programCore = new ProgramCore();
            programCore.GoToZoo();
            Console.ReadLine();
        }
    }

    class ProgramCore
    {
        private Zoo _zoo = new Zoo();
        private Menu _menu = new Menu();

        internal void GoToZoo()
        {
            _menu.ShowHeader();
            _zoo.PresentZoo();
            GoAviary();
        }

        private void GoAviary()
        {
            string aviaryString = _menu.ChoosingAnAviary();

            if (_zoo.IsThereAviary(aviaryString))
            {
                _menu.ShowAviary(aviaryString);
                _zoo.ShowAviares(aviaryString);
            }
            else
            {
                _menu.ErrorEnter();
            }
        }
    }

    class Menu
    {
        internal void ShowAnimal(Animal animal)
        {
            Console.WriteLine($"Парода - {animal.Name}   пол животного - {animal.Gender}   голос - {animal.Sound}");
        }

        internal void ShowAviary(string Animals)
        {
            Console.Clear();
            Console.WriteLine($"Вы подошли к вольеру с {Animals}");
            Console.WriteLine();
        }

        internal void ShowHeader()
        {
            Console.WriteLine("Мы представляем вам наш зоопарк\nвы можете посетить вольеры с животными");
            Console.WriteLine("Выберите какой из пердоставленных вольеров вы хотите посетить");
        }

        internal void ShowAviaries(string name)
        {
            Console.WriteLine($"Вольер с {name}");
        }

        internal string ChoseAviary()
        {
            Console.WriteLine("Выберите вольер для просмотра.");
            string choosing = Console.ReadLine();

            return choosing;
        }

        internal void ErrorEnter()
        {
            Console.WriteLine("Не верный ввод, попробуйте ещё раз.");
        }
    }

    class Zoo
    {
        private Menu _menu = new Menu();
        private List<Aviary> _Aviares = new List<Aviary>();

        private int _maximumAviary = 10;
        private int _minimumAviary = 4;


        internal Zoo()
        {
            Create();
        }

        internal void ShowAviares(string aviary)
        {
            foreach (Aviary item in _Aviares)
            {
                if (item.Name == aviary)
                {
                    item.ShowAnimals(item.Name);
                    break;
                }
            }
        }

        internal void PresentZoo()
        {
            foreach (Aviary aviary in _Aviares)
            {
                _menu.ShowAviaries(aviary.Name);
            }
        }

        internal bool IsThereAviary(string aviary)
        {
            bool isMatch = false;

            foreach (Aviary item in _Aviares)
            {
                if (item.Name == aviary)
                {
                    isMatch = true;
                }
            }
            return isMatch;
        }

        private void Create()
        {
            Random random = new Random();
            int randomQuantityAaviary = random.Next(_minimumAviary, _maximumAviary);

            for (int i = 0; i < randomQuantityAaviary; i++)
            {
                Aviary aviary = new Aviary();

                if (IsMatch(aviary) == false)
                {
                    _Aviares.Add(aviary);
                }
            }
        }

        private bool IsMatch(Aviary aviary)
        {
            bool match = false;

            foreach (Aviary tempAviary in _Aviares)
            {
                if (tempAviary.Name == aviary.Name)
                {
                    match = true;
                }
            }
            return match;
        }
    }

    class Aviary
    {
        private string[] _listAviary = { "Медведями", "Тиграми", "Зайцами", "Собаками", "Кошками", "Пантерами", "Страусами", "Крокодилами", "Чебурашками" };
        private int _maximumAnimals = 10;
        private int _minimumAnimals = 3;
        private List<Animal> _Animals = new List<Animal>();
        private List<int> _tempBaseId = new List<int>(0);
        private Random _random = new Random();
        private Menu _menu = new Menu();

        internal string Name { get; private set; }

        public Aviary()
        {
            Create();
        }

        internal void Create()
        {
            Name = GenerateName();
            GenerateAnimals();
        }

        internal void ShowAnimals(string name)
        {
            foreach (Animal animal in _Animals)
            {
                _menu.ShowAnimal(animal);
            }
        }

        private void GenerateAnimals()
        {
            int randomQuantityAnimals = _random.Next(_minimumAnimals, _maximumAnimals);

            for (int i = 0; i < randomQuantityAnimals; i++)
            {
                Animal animal = new Animal(GetIndex(Name));
                _Animals.Add(animal);
            }
        }

        private int GetIndex(string name)
        {
            int index = 0;

            for (int i = 0; i < _listAviary.Length; i++)
            {
                if (_listAviary[i] == name)
                {
                    index = i;
                }
            }
            return index;
        }

        private string GenerateName()
        {
            bool thereIsId = false;
            string name = "";
            int numberAviary = _random.Next(_listAviary.Length);

            foreach (int index in _tempBaseId)
            {
                if (index == numberAviary)
                {
                    thereIsId = true;
                }
            }

            if (thereIsId == false)
            {
                name = _listAviary[numberAviary];
                _tempBaseId.Add(numberAviary);
            }
            return name;
        }
    }

    class Animal
    {
        private string[] _listAnimal = { "Медведь", "Тигр", "Заяц", "Собака", "Кошка", "Пантера", "Страус", "Крокодил", "Чебурашка" };
        private string[] _listAnimalSounds = { "ЫЫЫ", "РРРР", "тратата", "гав гав", "мяуу", "мурррыы", "шшшш", "клац", "хочу в артелиристы" };
        private string[] _listGenders = { "Самка", "Самец" };

        private Random _random = new Random();

        internal string Name { get; private set; }

        internal string Gender { get; private set; }

        internal string Sound { get; private set; }

        internal Animal(int nameIndex)
        {
            Name = _listAnimal[nameIndex];
            Create();
        }

        private void Create()
        {
            GenerateSound();
            GenerateGender();
        }

        private void GenerateGender()
        {
            Gender = _listGenders[_random.Next(0, _listGenders.Length)];
        }

        private void GenerateSound()
        {
            for (int i = 0; i < _listAnimal.Length; i++)
            {
                if (Name == _listAnimal[i])
                {
                    Sound = _listAnimalSounds[i];
                }
            }
        }
    }
}
