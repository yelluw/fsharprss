// Learn more about F# at http://fsharp.org

open System
open System.Text
open System.Net.Http
open Rss

open Newtonsoft.Json

[<EntryPoint>]
let main argv =

    let doXmlStuff = async { 
            let client = new  HttpClient()
            let! response = client.GetByteArrayAsync("http://pablojuan.com/feed/")                         
                            |> Async.AwaitTask                                                     
            let content = Encoding.UTF8.GetString(response,0, (response.Length - 1))                 
           
            let parsedStuff = deserializeXml content 
            let json = JsonConvert.SerializeObject(parsedStuff)
            return json
            
        }

    doXmlStuff 
    |> Async.RunSynchronously
    |> printfn "%s"       
    0 // return an integer exit code