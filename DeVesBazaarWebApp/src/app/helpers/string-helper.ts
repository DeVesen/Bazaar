
export class StringHelper {

    public static containsIgnorCase(value: string, searchFor: string): boolean {
        const valueGiven = value ? value.trim().length > 0 : false;
        const searchForGiven = searchFor ? searchFor.trim().length > 0 : false;

        return (valueGiven && searchForGiven)
            ? value.toLowerCase().indexOf(searchFor.toLowerCase()) >= 0
            : false;
    }

}