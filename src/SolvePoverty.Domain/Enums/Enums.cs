namespace SolvePoverty.Domain.Entities;

public enum UserType
{
    PersonInNeed,
    Volunteer,
    Organization,
    Donor,
    JobPoster
}

public enum RequestType
{
    Food,
    Clothing,
    Shelter,
    Job,
    Training,
    Medical,
    Financial,
    Other
}

public enum RequestStatus
{
    Open,
    InProgress,
    Fulfilled,
    Closed
}

public enum OfferType
{
    Food,
    Clothing,
    Shelter,
    Job,
    Training,
    Financial,
    Service,
    Other
}

public enum OfferStatus
{
    Available,
    Pending,
    Accepted,
    Completed,
    Cancelled
}

public enum JobType
{
    FullTime,
    PartTime,
    Contract,
    Temporary,
    Internship
}

public enum JobStatus
{
    Open,
    Closed,
    Filled
}

public enum ApplicationStatus
{
    Submitted,
    UnderReview,
    Interviewed,
    Accepted,
    Rejected
}

public enum DonationStatus
{
    Pending,
    Completed,
    Failed,
    Refunded
}

public enum OrganizationType
{
    FoodBank,
    Shelter,
    CharityOffice,
    HealthClinic,
    EmploymentCenter,
    CommunityCenter,
    Other
}