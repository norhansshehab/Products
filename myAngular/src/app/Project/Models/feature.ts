import { CategoryFeature } from './category-feature';
import { ProductFeatureValue } from './product-feature-value';

export class Feature {
    constructor(
        public FeatureID: number, 
        public FeatureName: string,
        public CategoryFeatures? : CategoryFeature[],
        public ProductFeatureValues? : ProductFeatureValue[]
       
        ) { }
}
