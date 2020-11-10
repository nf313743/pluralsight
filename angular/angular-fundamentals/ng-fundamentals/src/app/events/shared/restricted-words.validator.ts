import { FormControl } from "@angular/forms";

export function restrictedWords(words: string[]) {
  return (control: FormControl): { [key: string]: any } => {
    if (!words) {
      return null;
    }

    const invalidWords = words
      .map(x => (control.value.includes(x) ? x : null))
      .filter(x => x !== null);

    return invalidWords && invalidWords.length > 0
      ? { restrictedWords: invalidWords.join(", ") }
      : null;
  };
}
