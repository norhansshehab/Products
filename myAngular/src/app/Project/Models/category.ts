import { CategoryFeature } from './category-feature';
import { Product } from './product';

export class Category {
    constructor(
        public CatID : number,
        public CatName : string,
        public ParentID? : number,
        public ParentCategory? : Category,
        public CategoryFeatures? : CategoryFeature[],
        public Products? : Product[],
    ){}
}
