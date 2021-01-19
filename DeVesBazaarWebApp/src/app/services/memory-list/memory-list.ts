
export interface IMemoryListOptions<TElem> {
    getItemById(lst: TElem[], id: number): TElem;
    
    getItemByRef(lst: TElem[], reference: any): TElem;

    updateItem(lstElem: TElem, source: any): void;
}

export class MemoryList<T> {
    
    constructor(private innerLst: T[],
                private options: IMemoryListOptions<T>) {}


    get(id: number): T {
        return this.options.getItemById(this.innerLst, id);
    }

    getAll(): T[] {
        return this.innerLst;
    }


    add(newItem: T): void {
        this.innerLst.push(newItem);
    }
    update(item: T): void {
        const itemToUpdate = this.options.getItemByRef(this.innerLst, item);
        if (itemToUpdate) {
            this.options.updateItem(itemToUpdate, item);
        }
    }
    remove(id: number): void {
        const itemToRemove = this.get(id);
        if (itemToRemove) {
            const index = this.innerLst.indexOf(itemToRemove, 0);
            if (index > -1) {
                this.innerLst.splice(index, 1);
            }
        }
    }


}