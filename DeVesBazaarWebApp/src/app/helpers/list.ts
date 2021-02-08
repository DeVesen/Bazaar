export class List<T> {
    private innerLst: T[] = [];
  
    constructor(initialList?: T[]) {
      this.innerLst = initialList ? initialList : [];
    }
  
    get length(): number {
      return this.innerLst.length;
    }
  
    public push(...items: T[]): number {
      return this.innerLst.push(...items);
    }
  
    public remove(predicate: (value: T, index: number, array: T[]) => unknown, thisArg?: any): number {
      const entries = this.innerLst.filter(predicate, thisArg);
      if (entries) {
        entries.forEach(x => {
          const xIndex = this.innerLst.indexOf(x);
          if (xIndex >= 0) {
            this.innerLst.splice(xIndex, 1);
          }
        });
        return entries.length;
      }
      return 0;
    }
  
    public all(): T[] {
      return this.innerLst;
    }
  
    public get(index: number): T {
      return this.innerLst[index];
    }

    public getIndex(predicate: (value: T, index: number, obj: T[]) => unknown, thisArg?: any): number {
      const entry = this.innerLst.find(predicate, thisArg);
      return entry ? this.innerLst.indexOf(entry) : -1;
    }
  
    public forEach(callbackfn: (value: T, index: number, array: T[]) => void, thisArg?: any): void {
      this.innerLst.forEach(callbackfn, thisArg);
    }
  
    public exists(predicate: (value: T, index: number, obj: T[]) => unknown, thisArg?: any): boolean {
      return this.innerLst.find(predicate, thisArg) !== undefined;
    }
  
    public filter(predicate: (value: T, index: number, array: T[]) => unknown, thisArg?: any): T[] {
      return this.innerLst.filter(predicate, thisArg);
    }
  
    public find(predicate: (value: T, index: number, obj: T[]) => unknown, thisArg?: any): T | undefined {
      return this.innerLst.find(predicate, thisArg);
    }
  
    public sort(compareFn?: (a: T, b: T) => number): void {
      this.innerLst = this.innerLst.sort(compareFn);
    }
  }
