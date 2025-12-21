namespace Kosha.CustomerManager.WindowsClient.Models.Entities;

public class Task
{
    public int Id { get; set; }

    public string Title { get; set; }

    public string Description { get; set; }

    public bool IsComplete { get; set; }

    public TaskState State { get; set; }

    public TaskCategory Category { get; set; }

    public TaskImportance Importance { get; set; }
}

public enum TaskState
{
    InProgress,
    Completed,
    NotStarted,
    Late,
    Archive,
    Deleted
}

public enum TaskCategory
{
    Work,
    Personal,
    Home,
    HealthWellbeing,
    Finance,
    Shopping,
    SocialFamily,
    Education,
    Travel,
    Errand,
    HobbiesLeisure,
    VolunteeringCommunity,
    BirthdaysAnniversaries,
    Projects,
    LongTermGoals
}

public enum TaskImportance
{
    Low,
    Medium,
    High,
    Critical
}