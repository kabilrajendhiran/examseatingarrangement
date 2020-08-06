import {SeatingModel} from './SeatingModel';
import {DepartmentModel} from './DepartmentModel';
import {ExamModel} from './ExamModel';

export class AllocationSeatingModel {
  constructor(
    public seating:SeatingModel,
    public department:DepartmentModel,
    public exam:ExamModel
  ) {}
}
