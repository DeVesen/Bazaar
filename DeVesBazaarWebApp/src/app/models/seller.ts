
export interface ISeller {
  id: number;
  salutation: string;
  firstName: string;
  lastName: string;
  street?: string;
  zip?: string;
  town?: string;
  phone?: string;
  eMail?: string;
}