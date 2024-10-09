import { Freelancer } from './freelancer';
import { Client } from './client';
import { Skill } from './skill';
import { Bid } from './bid';

export interface Project {
  Id: number;
  Title: string;
  Description: string;
  ClientId: number;
  Client: Client;
  SelectedFreelancerId?: number;
  SelectedFreelancer?: Freelancer;
  RequiredSkills: Skill[];
  Budget: number;
  Status:String;
  Bids:Bid[];
}
