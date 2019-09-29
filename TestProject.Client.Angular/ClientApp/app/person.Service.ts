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

    createPerson(person: Person) {
        return this.http.post(this.url, person);
    }
    updatePerson(person: Person) {

        return this.http.put(this.url + '/' + person.id, person);
    }
    deletePerson(id: number) {
        return this.http.delete(this.url + '/' + id);
    }
}