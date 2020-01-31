using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using Microsoft.AspNetCore.Components.Web;

namespace ComponentRenderTreeBuilder.Shared
{
    public class ComponentTable : ComponentBase
    {
        [Parameter] public int cols { get; set; } = 5;
        [Parameter] public int rows { get; set; } = 5;

        [Parameter] public EventCallback<string> CellClicked { get; set; }

        protected override void BuildRenderTree(RenderTreeBuilder builder)
        {
            var seq = 0;

            builder.OpenElement(++seq, "div");
            builder.AddAttribute(++seq, "class", "divTable");

            builder.OpenElement(++seq, "div");
            builder.AddAttribute(++seq, "class", "divHeader");

            for (int col = 0; col < cols; col++)
            {
                builder.OpenElement(++seq, "div");
                builder.AddAttribute(++seq, "class", "divCell");
                builder.AddContent(++seq, $"col {col}");
                builder.CloseElement(); // divCell
            }

            builder.CloseElement(); // divHeader


            for (int row = 0; row < rows; row++)
            {
                builder.OpenElement(++seq, "div");
                builder.AddAttribute(++seq, "class", "divRow");

                for (int col = 0; col < cols; col++)
                {
                    string contenu = $"cell {row}/{col}";

                    builder.OpenElement(++seq, "div");
                    builder.AddAttribute(++seq, "class", "divCell noselect");
                    builder.AddAttribute(++seq, "onclick", EventCallback.Factory.Create<MouseEventArgs>(this, () => CellClickedInternal(contenu)));
                    builder.AddContent(++seq, contenu);
                    builder.CloseElement(); // divCell
                }
                builder.CloseElement(); // divCell
            }

            builder.CloseElement(); // divTable

            base.BuildRenderTree(builder);  
        }

        private async Task CellClickedInternal(string value)
        {
            await CellClicked.InvokeAsync(value);
        }
    }
}
