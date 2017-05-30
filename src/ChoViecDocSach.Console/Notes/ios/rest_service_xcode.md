@IBAction func btnTranslate(_ sender: Any) {        

        let input: String = self.lblText.text!

        // prepare json data
        let json: [String: Any] = ["text": input,
                                   "from": "en",
                                   "to": "vi"]        

        let jsonData = try? JSONSerialization.data(withJSONObject: json)        

        // create post request
        let url = URL(string: "http://www.transltr.org/api/translate")!

        var request = URLRequest(url: url)

        request.httpMethod = "POST"
        request.addValue("application/json", forHTTPHeaderField: "Content-Type")
        request.addValue("application/json", forHTTPHeaderField: "Accept")        

        // insert json data to the request
        request.httpBody = jsonData        

        let task = URLSession.shared.dataTask(with: request) { data, response, error in            

            // background thread 
            // https://stackoverflow.com/questions/37805885/how-to-create-dispatch-queue-in-swift-3
            let backgroundQueue = DispatchQueue(label: "backgroundQueue")
            backgroundQueue.async {                

                guard let data = data, error == nil else {
                    print(error?.localizedDescription ?? "No data")
                    return
                }

                let responseJSON = try? JSONSerialization.jsonObject(with: data, options: [])

                if let responseJSON = responseJSON as? [String: Any] {

                    print(responseJSON)

                    // parse json: http://roadfiresoftware.com/2016/12/how-to-parse-json-with-swift-3/
                    if let translationText = responseJSON["translationText"] as? String
                    {
                        // get back to the main thread
                        // https://stackoverflow.com/questions/37805885/how-to-create-dispatch-queue-in-swift-3
                        DispatchQueue.main.async {
                            self.lblOutput.text = translationText;
                        }
                    }
                }
            }
        }        

        task.resume()    
    }

    