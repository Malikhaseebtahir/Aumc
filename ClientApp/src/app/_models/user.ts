import { Photo } from './photo';
import { Address } from './Address';
import { Interest } from './Interest';

export interface User {
  id: number;
  userName: string;
  teacherOrStudent: string;
  knownAs: string;
  age: number;
  gender: string;
  created: Date;
  lastActive: Date;
  address: Address;
  className: string;
  photoUrl: string;
  interest?: Interest;
  introduction?: string;
  lookingFor?: string;
  photos?: Photo[];
  roles?: string[];
}
