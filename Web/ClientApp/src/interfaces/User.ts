interface IUserRoot {
  users: IUser[];
}

interface IUser {
  username: string,
  type: string
}
export {
  IUser,
  IUserRoot
  }
