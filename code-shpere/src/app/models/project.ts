import { Freelancer } from './freelancer';
import { Client } from './client';
import { RequiredSkill } from './required-skill';

export interface Project {
  id: number;
  title: string;
  description: string;
  clientId: number;
  client: Client;
  freelancerId?: number;
  freelancer?: Freelancer;
  requiredSkills: RequiredSkill[];
  budget: number;
  deadline: Date;
}
