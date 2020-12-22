export interface IUser {
  token?: string;
  er_role: string;
  name: string;
  id?: string;
  user_id?: string;
}

export interface IUserInfo {
  email: string;
  roles: any;
  userName: string;
  id: number;
}
