import { Skill } from './skill';

export interface RequiredSkill {
  id: number;
  skillId: number;
  skill: Skill;
  projectId: number;
}
