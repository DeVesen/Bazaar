
export class StringHelper {

    public static containsIgnorCase(value: string, searchFor: string): boolean {
        const valueGiven = value ? value.trim().length > 0 : false;
        const searchForGiven = searchFor ? searchFor.trim().length > 0 : false;

        return (valueGiven && searchForGiven)
            ? value.toLowerCase().indexOf(searchFor.toLowerCase()) >= 0
            : false;
    }

    public static valuesAsArray(objValue: any): string[] {
        const properties: string[] = [];
        Object.keys(objValue).forEach((p: string) => {
        const pV = objValue[p]?.toString();
        properties.push(pV);
        });
        return properties;
    }

    public static objectContainsValue(objValue: any, simpleValue: string): boolean {
        if (objValue && simpleValue) {
            const properties = StringHelper.valuesAsArray(objValue)?.filter(p => p ? true : false) ?? [];
            const existing = properties.find(p => p.toLowerCase().indexOf(simpleValue.toLowerCase()) >= 0) ?? '';
            return existing.toLowerCase().indexOf(simpleValue.toLowerCase()) >= 0;
        }
        return false;
    }
}