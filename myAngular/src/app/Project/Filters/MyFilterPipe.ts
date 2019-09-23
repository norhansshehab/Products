import { Pipe, PipeTransform } from '@angular/core';
import { ProductFeatureValue } from '../Models/product-feature-value';
import { Feature } from '../Models/feature';

@Pipe({
    name: 'myfilter',
    pure: false
})

export class MyFilterPipe implements PipeTransform {
    transform(ProdFeatureValue_Arr: ProductFeatureValue[], FeatureID: number): any {
        //debugger;

        if (ProdFeatureValue_Arr) {
            return ProdFeatureValue_Arr.filter((pfv: any) => pfv.FeatureID === FeatureID);
        }
        // if (!items || !filter) {
        //     return items;
        // }
        //return items.indexOf(filter.FeatureID) !== -1;  

        //return items.forEach(pfv => { pfv.FeatureID === filter.FeatureID });

    }
}  