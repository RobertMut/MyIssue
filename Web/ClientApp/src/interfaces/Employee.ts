interface IEmployeeRoot {
  employees: IEmployee[];
}

interface IEmployee {
  name: string,
  surname: string,
  no: string,
  position: string
}

export {
  IEmployee,
  IEmployeeRoot
  }
