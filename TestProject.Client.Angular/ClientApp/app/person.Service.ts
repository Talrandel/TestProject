import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Person } from './person';

@Injectable()
export class PersonService {

    private url = "/api/person";

    constructor(private http: HttpClient) {
    }

    getPersons() {
        return this.http.get(this.url);
    }

    //createProduct(product: Product) {
    //    return this.http.post(this.url, product);
    //}
    //updateProduct(product: Product) {

    //    return this.http.put(this.url + '/' + product.id, product);
    //}
    //deleteProduct(id: number) {
    //    return this.http.delete(this.url + '/' + id);
    //}
}