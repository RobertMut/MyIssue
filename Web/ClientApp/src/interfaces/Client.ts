interface IClientRoot {
  clients: IClient[];
}
interface IClient {
  id: number,
  name: string,
  country: string,
  no: string,
  street: string,
  streetNo: string,
  flatNo: string,
  description: string;
}
export {
  IClient,
  IClientRoot
  }
