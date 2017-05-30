
//: Playground - noun: a place where people can play

import UIKit
import PlaygroundSupport

PlaygroundPage.current.needsIndefiniteExecution = true
URLCache.shared = URLCache(memoryCapacity: 0, diskCapacity: 0, diskPath: nil)

/*

let url = URL(string: "https://en.wikipedia.org/w/api.php?format=json&action=query&titles=London&prop=images")

let request = URLRequest(url: url!)

let task = URLSession.shared.dataTask(with: request) { (data, response, error) in

    if let data = data {
        let json = try? JSONSerialization.jsonObject(with: data, options: [])        

        if let json = json as? [String:Any]
        {
            print(json)
        }
    }
}

task.resume()

*/

// prepare json data

//let json = "[\"text\": \"hello\", \"from\": \"en\", \"to\":\"ja\"]"
//let jsonData = json.data(using: .utf8)

let json : [String: Any] = ["text": "hello", "from": "en", "to":"te"]
let jsonData = try? JSONSerialization.data(withJSONObject: json)

// create post request
let url = URL(string: "http://www.transltr.org/api/translate")!

var request = URLRequest(url: url)

request.addValue("application/json", forHTTPHeaderField: "Content-Type")
request.addValue("application/json", forHTTPHeaderField: "Accept")
request.httpMethod = "POST"


// insert json data to the request

request.httpBody = jsonData

let task = URLSession.shared.dataTask(with: request) { (data, response, error) in

    guard let data = data, error == nil else {
        print(error?.localizedDescription ?? "No data")
        return
    } 

    let responseJSON = try? JSONSerialization.jsonObject(with: data, options:.allowFragments)   

    if let responseJSON = responseJSON as? [String: Any] {
        print(responseJSON)
    }
}
 
task.resume()