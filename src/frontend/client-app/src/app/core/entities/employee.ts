export interface Employee
{
    id: string
    name?: string
    lastname?: string
    phoneNumber?: string
    positionId: string
}

export interface EmployeeCreate
{
    name?: string
    lastname?: string
    phoneNumber?: string
    positionId: string
}

export interface EmployeeUpdate
{
    id: string
    name?: string
    lastname?: string
    phoneNumber?: string
    positionId: string
}