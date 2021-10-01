export interface IEmployeeRoot {
  employees: IEmployee[];
}

export interface IEmployee {
  login: string,
  name: string,
  position: string
}
