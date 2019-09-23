import { ProductFeatureValue } from './product-feature-value';

export class Product {
    constructor(
        public ProdID :number,
        public ProdName : string,
        public CatID? : number,
        public ProductFeatureValues? : ProductFeatureValue[]
    ){}
}
