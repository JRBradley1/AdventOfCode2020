fn main() {
    let input = [
        "su",
        "ab"
    ];

    let mut running_question_total = 0;
    let mut current_person_questions = Vec::<char>::with_capacity(26);
    let mut isFirst = true;
    for row_data in input.iter() {
        if row_data.is_empty() {
            running_question_total += current_person_questions.len();
            current_person_questions.clear();
            isFirst = true;
            continue;
        }
        if isFirst {
            for character in row_data.chars() {
                current_person_questions.push(character);
            }
            isFirst = false;
        }

        if !isFirst {
            let current_person_questions_copy = current_person_questions.clone();
            for question in current_person_questions_copy.iter() {
                if !row_data.contains(*question) {
                    current_person_questions.retain(|&q| q != *question);
                }
            }
        }
    }

    println!("Total Yes: {:?}", running_question_total);
}
