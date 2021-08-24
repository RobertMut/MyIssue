using API.Database.Models;

namespace API.Database
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly MyIssueContext context;
        private Repository<User> userRepository;
        private Repository<Task> taskRepository;
        private Repository<Client> clientRepository;
        private Repository<Employee> employeeRepository;
        private Repository<TaskType> taskTypeRepository;

        public UnitOfWork(MyIssueContext context)
        {
            this.context = context;
        }
        public Repository<User> UserRepository
        {
            get
            {
                if(userRepository is null)
                {
                    userRepository = new Repository<User>(context);
                }
                return userRepository;
            }
        }
        public Repository<Task> TaskRepository
        {
            get
            {
                if (taskRepository is null)
                {
                    taskRepository = new Repository<Task>(context);
                }
                return taskRepository;
            }
        }
        public Repository<Employee> EmployeeRepository
        {
            get
            {
                if (employeeRepository is null)
                {
                    employeeRepository = new Repository<Employee>(context);
                }
                return employeeRepository;
            }
        }
        public Repository<Client> ClientRepository
        {
            get
            {
                if (clientRepository is null)
                {
                    clientRepository = new Repository<Client>(context);
                }
                return clientRepository;
            }
        }
        public Repository<TaskType> TaskTypeRepository
        {
            get
            {
                if (taskTypeRepository is null)
                {
                    taskTypeRepository = new Repository<TaskType>(context);
                }

                return taskTypeRepository;
            }
        }
        public int Complete()
        {
            return context.SaveChanges();
        }
        public void Dispose()
        {
            context.Dispose();
        }
    }
}
